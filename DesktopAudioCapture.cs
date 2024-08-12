namespace AudioToSerial
{
	using NAudio.Dsp;
	using NAudio.Wave;

	/// <summary>
	/// Make for an easy way to capture realtime audio on desktop.
	/// </summary>
	public class DesktopAudioCapture
	{
		private readonly WasapiLoopbackCapture capture;
		private readonly BufferedWaveProvider buffer;
		private readonly object lockObject = new object();

		// expose to make easier
		public WaveFormat WaveFormat;

		public DesktopAudioCapture()
		{
			capture = new WasapiLoopbackCapture();
			buffer = new BufferedWaveProvider(capture.WaveFormat);

			this.WaveFormat = capture.WaveFormat;
			capture.DataAvailable += Capture_DataAvailable;
		}

		public void Start()
		{
			capture.StartRecording();
		}

		private void Capture_DataAvailable(object? sender, WaveInEventArgs e)
		{
			lock (lockObject)
			{
				buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
				// ApplyFFT(e.Buffer, e.BytesRecorded);
			}
		}

		private void ApplyFFT(byte[] buffer, int bytesRecorded)
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
			// Process fftBuffer as needed
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

		public void StopCapture()
		{
			capture.StopRecording();
			capture.Dispose();
		}
	}
}
