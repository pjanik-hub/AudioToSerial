namespace AudioToSerial
{
	using NAudio.Dsp;
	using NAudio.Wave;

	/// <summary>
	/// Make for an easy way to capture realtime audio on desktop.
	/// </summary>
	public class DesktopAudioCapture
	{
		private FrequencyBuckets FrequencyAmplitudes { get; set; }

		private const int FFT_SAMPLE_RATE = 44100;
		private readonly WasapiLoopbackCapture capture;
		private readonly BufferedWaveProvider buffer;
		private readonly object lockObject = new object();
		private const double SCALE_FACTOR = 1E6;

		// expose to make easier
		public WaveFormat WaveFormat;

		public DesktopAudioCapture()
		{
			capture = new WasapiLoopbackCapture();
			buffer = new BufferedWaveProvider(capture.WaveFormat);

			this.WaveFormat = capture.WaveFormat;
			capture.DataAvailable += Capture_DataAvailable;

			FrequencyAmplitudes = new FrequencyBuckets { Low = 0, Mid = 0, High = 0 };
		}

		/// <summary>
		/// Start the desktop audio recording
		/// </summary>
		public void Start()
		{
			capture.StartRecording();
		}

		/// <summary>
		/// Fired when data from the WasapiLoopbackCapture is available, 
		/// stores results in a buffer.
		/// FFT results can be retrieved with <see cref="GetFrequencyBuckets">/
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Capture_DataAvailable(object? sender, WaveInEventArgs e)
		{
			lock (lockObject)
			{
				buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
				FrequencyAmplitudes = ApplyFFT(e.Buffer, e.BytesRecorded);
			}
		}

		/// <summary>
		/// Apply FFT processing
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="bytesRecorded"></param>
		/// <returns></returns>
		private static FrequencyBuckets ApplyFFT(byte[] buffer, int bytesRecorded)
		{
			// Convert the byte array to float samples
			int samplesRecorded = bytesRecorded / sizeof(float);
			float[] samples = new float[samplesRecorded];
			Buffer.BlockCopy(buffer, 0, samples, 0, bytesRecorded);

			// Apply FFT
			Complex[] fftBuffer = new Complex[samples.Length];
			for (int i = 0; i < samples.Length; i++)
			{
				fftBuffer[i].X = samples[i];
				fftBuffer[i].Y = 0;
			}

			FastFourierTransform.FFT(
				true,
				(int)Math.Log(samples.Length, 2),
				fftBuffer
			);

			// fftBuffer now contains frequency domain data
			// Calculate magnitude (amplitude) of each freqency

			double[] magnitudes = new double[fftBuffer.Length / 2];
			for (int i = 0; i < magnitudes.Length; i++)
			{
				double xSquared = fftBuffer[i].X * fftBuffer[i].X;
				double ySquared = fftBuffer[i].Y * fftBuffer[i].Y;

				magnitudes[i] = Math.Sqrt(xSquared + ySquared) * SCALE_FACTOR;
			}

			// Finally, convert to amplitude buckets!
			return AggregateFrequencyBands(magnitudes);
		}

		/// <summary>
		/// Calculate start/end points of each bucket, then return the 
		/// amplitude of each freq. band.
		/// </summary>
		/// <param name="magnitudes"></param>
		/// <returns></returns>
		private static FrequencyBuckets AggregateFrequencyBands(double[] magnitudes)
		{
			// 0.00 to 300 Hz
			int lowStart = 0;
			int lowEnd = 300;

			// 301 to 2.00 kHz
			int midStart = 301;
			int midEnd = 2000;

			// 2.01 to 20.0 kHz
			int highStart = 2001;
			int highEnd = 20000;

			int doubleMagnitudeLen = magnitudes.Length * 2;

			int lowStartBin = FrequencyMap(lowStart, FFT_SAMPLE_RATE, doubleMagnitudeLen);
			int lowEndBin = FrequencyMap(lowEnd, FFT_SAMPLE_RATE, doubleMagnitudeLen);
			int midStartBin = FrequencyMap(midStart, FFT_SAMPLE_RATE, doubleMagnitudeLen);
			int midEndBin = FrequencyMap(midEnd, FFT_SAMPLE_RATE, doubleMagnitudeLen);
			int highStartBin = FrequencyMap(highStart, FFT_SAMPLE_RATE, doubleMagnitudeLen);
			int highEndBin = FrequencyMap(highEnd, FFT_SAMPLE_RATE, doubleMagnitudeLen);

			// get averages
			double lowAmplitude = AggregateMagnitude(magnitudes, lowEndBin, lowStartBin);
			double midAmplitude = AggregateMagnitude(magnitudes, midEndBin, midStartBin);
			double highAmplitude = AggregateMagnitude(magnitudes , highEndBin, highStartBin);

			return new FrequencyBuckets
			{
				Low = lowAmplitude,
				Mid = midAmplitude,
				High = highAmplitude
			};
		}


		public static int FrequencyMap(int freq, int sampleRate, int fftSize)
		{
			return (int)(freq / (sampleRate / (double)fftSize));
		}

		/// <summary>
		/// Get the average amplitude in a band.
		/// </summary>
		/// <param name="magnitudes">The collection of bands</param>
		/// <param name="startBin"></param>
		/// <param name="endBin"></param>
		/// <returns></returns>
		private static double AggregateMagnitude(double[] magnitudes, int endBin, int startBin)
		{
			double sum = 0;

			for (int i = startBin; i <= endBin; i++)
			{
				sum += magnitudes[i];
			}

			return sum / (endBin - startBin + 1); // Average amplitude in the band
		}

		/// <summary>
		/// Public-facing function to provide buffer access
		/// </summary>
		/// <returns></returns>
		public byte[] GetBufferForWaveViewer()
		{
			if (buffer == null)
				return Array.Empty<byte>();

			byte[] tempBuffer;

			// put a lock on this process to make it thread safe
			lock (lockObject)
			{
				tempBuffer = new byte[buffer.BufferLength];
				int bytesRead = buffer.Read(tempBuffer, 0, tempBuffer.Length);
				Array.Resize(ref tempBuffer, bytesRead);
			}

			return tempBuffer;
		}

		/// <summary>
		/// Thread-safe way to retrieve the frequency bucket
		/// </summary>
		/// <returns>The average high, mid, and low freqs</returns>
		public FrequencyBuckets GetFrequencyBuckets()
		{
			FrequencyBuckets tempBucket;

			// I think this is needed
			lock (lockObject)
			{
				// Assume it's null first
				tempBucket = new FrequencyBuckets
				{
					Low = FrequencyAmplitudes?.Low ?? 0.0,
					Mid = FrequencyAmplitudes?.Mid ?? 0.0,
					High = FrequencyAmplitudes?.High ?? 0.0
				};
			}

			return tempBucket;
		}

		public void StopCapture()
		{
			capture.StopRecording();
			capture.Dispose();
		}
	}

	/// <summary>
	/// A simple class to house the freq. bucket values (amplitudes)
	/// </summary>
	public class FrequencyBuckets
	{
		public double Low { get; set; }

		public double Mid { get; set; }

		public double High { get; set; }
	}
}
