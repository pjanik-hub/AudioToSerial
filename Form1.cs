namespace AudioToSerial
{
	using FftSharp.Windows;
	using NAudio.Wave;
	using System.IO.Ports;
	using System.Numerics;

	public partial class AudioApp : Form
	{
		SerialPort? currentPort = null;
		readonly FrequencyBuckets FrequencyBuckets;
		readonly FrequencyToSerial audioToPort;

		const int BAUD_RATE = 9600;
		const int SAMPLE_RATE = 44100;
		const int BUFFER_MS = 120;

		readonly WasapiLoopbackCapture Audio_Capture;
		readonly double[] Audio_Data;
		readonly double[] FFT_Data;
		readonly double[] FFT_Freqs;
		readonly int Audio_Data_Length = SAMPLE_RATE * BUFFER_MS / 1000;
		readonly int FFT_Data_Length;
		readonly int FFT_Freqs_Length;

		public AudioApp()
		{
			InitializeComponent();

			Audio_Data = new double[Audio_Data_Length];

			double[] paddedAudio = FftSharp.Pad.ZeroPad(Audio_Data);
			Complex[] complexData = FftSharp.FFT.Forward(paddedAudio);

			double[] fftAmplitudes = FftSharp.FFT.Power(complexData);
			double[] fftFrequencyScale = FftSharp.FFT.FrequencyScale(fftAmplitudes.Length, SAMPLE_RATE);

			FFT_Data_Length = fftAmplitudes.Length;
			FFT_Data = new double[fftAmplitudes.Length];

			FFT_Freqs_Length = fftFrequencyScale.Length;
			FFT_Freqs = new double[fftFrequencyScale.Length];

			fftPlot.Plot.Add.SignalXY(FFT_Freqs, FFT_Data);
			fftPlot.Plot.YLabel("PWR (RMS)");
			fftPlot.Plot.XLabel("Freq. (Hz)");
			fftPlot.Plot.Axes.SetLimits(
				0,
				20_000,
				0,
				120
			);
			fftPlot.Refresh();

			this.Audio_Capture = new WasapiLoopbackCapture()
			{
				WaveFormat = new WaveFormat(SAMPLE_RATE, 16, 1),
			};
			this.Audio_Capture.DataAvailable += Audio_Capture_DataAvailable;

			FrequencyBuckets = new FrequencyBuckets();
			audioToPort = new FrequencyToSerial();

			timer1.Interval = 40;
		}

		private void Audio_Capture_DataAvailable(object? sender, WaveInEventArgs e)
		{
			int sampleCount = e.Buffer.Length / 2;

			for (int i = 0; i < Audio_Data_Length && i < sampleCount; i++)
				Audio_Data[i] = BitConverter.ToInt16(e.Buffer, i * 2);
		}

		/// <summary>
		/// Initial setup of arrays/data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AudioApp_Load(object sender, EventArgs e)
		{
			Refresh_SerialPorts(true);
			EmptyFillAudioData();

			Audio_Capture.StartRecording();

			timer1.Start();
		}

		private void EmptyFillAudioData()
		{
			for (int i = 0; i < Audio_Data_Length; i++)
				Audio_Data[i] = 0;
		}

		private void Refresh_SerialPorts(bool firstTime = false)
		{
			string[] ports = SerialPort.GetPortNames();

			serialsCombo.Items.Clear();
			serialsCombo.Items.AddRange(ports);

			if (firstTime)
				bindResult.Text = "";
			else
				bindResult.Text = "Refreshed ports!";
		}

		private void refreshSerialButton_Click(object sender, EventArgs e)
		{
			Refresh_SerialPorts();
		}

		private void bindButton_Click(object sender, EventArgs e)
		{
			string? portName = (string?)serialsCombo.SelectedItem;

			if (portName == null)
			{
				bindResult.Text = "Invalid port name: NULL";
				return;
			}

			try
			{
				audioToPort.ConnectPort(portName, BAUD_RATE);
				bindResult.Text = $"Successfully connected to {portName}!";
			}
			catch
			{
				bindResult.Text = $"Invalid port name: {portName}";
			}
		}

		private void AudioApp_FormClosing(object sender, FormClosingEventArgs e)
		{
			Audio_Capture.StopRecording();
			Audio_Capture.Dispose();

			if (currentPort != null && currentPort.IsOpen)
				currentPort.Close();

			timer1.Stop();
			timer1.Dispose();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				// window the data first
				Hanning window = new();
				window.Create(Audio_Data_Length);
				window.ApplyInPlace(Audio_Data);

				// pad (to nearest power of 2) and prep data
				double[] paddedAudio = FftSharp.Pad.ZeroPad(Audio_Data);
				Complex[] complexData = FftSharp.FFT.Forward(paddedAudio);

				// calculate amplitudes based on checkbox
				double[] fftAmplitudes = this.powerCheckBox.Checked ?
					FftSharp.FFT.Power(complexData) :    // dB
					FftSharp.FFT.Magnitude(complexData); // RMS
				double[] fftFrequencyScale = FftSharp.FFT.FrequencyScale(fftAmplitudes.Length, SAMPLE_RATE);

				// finally, place data into collections the Plot can read
				Array.Copy(fftAmplitudes, FFT_Data, FFT_Data_Length);
				Array.Copy(fftFrequencyScale, FFT_Freqs, FFT_Freqs_Length);

				// some simple auto-scaling
				if (this.autoScaleCheckBox.Checked)
				{
					double fftPeakAmp = fftAmplitudes.Max();
					double plotYMax = fftPlot.Plot.Axes.GetLimits().Top;

					ResetYAxisScaling((int) fftPeakAmp, (int) plotYMax);
				}

				var bucket = CalculateFrequencyBuckets(fftFrequencyScale, fftAmplitudes);

				if (bucket.Low >= 5)
					this.lowTxtBx.Text = bucket.Low.ToString("0.##E+0");
				else
					this.lowTxtBx.Text = "0.00";

				if (bucket.Mid >= 5)
					this.midTxtBx.Text = bucket.Mid.ToString("0.##E+0");
				else
					this.midTxtBx.Text = "0.00";

				if (bucket.High >= 5)
					this.highTxtBx.Text = bucket.High.ToString("0.##E+0");
				else
					this.highTxtBx.Text = "0.00";

				fftPlot.Refresh();
			}
			catch
			{
				System.Diagnostics.Debug.WriteLine("Error in timer");
			}
		}

		private void powerCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.powerCheckBox.Checked)
				this.fftPlot.Plot.YLabel("PWR (dB)");
			else
				this.fftPlot.Plot.YLabel("PWR (RMS)");

			ResetYAxisScaling();
		}

		static FrequencyBuckets CalculateFrequencyBuckets(double[] fftXVals, double[] fftYVals)
		{
			int lowStart = 0;
			int lowEnd = 400;

			int midStart = 401;
			int midEnd = 2000;

			int highStart = 2001;
			int highEnd = 20000;

			int lowStartIndex = 0;
			int lowEndIndex = GetClosestNumberIndex(fftXVals, lowEnd);

			int midStartIndex = lowEndIndex + 1;
			int midEndIndex = GetClosestNumberIndex(fftXVals, midEnd);

			int highStartIndex = midEndIndex + 1;
			int highEndIndex = fftXVals.Length - 1;

			if (lowEndIndex <= lowStartIndex)
				throw new Exception();
			if (midEndIndex <= midStartIndex)
				throw new Exception();
			if (highEndIndex <= highStartIndex)
				throw new Exception();

			double lowVal = AggregateValues(fftYVals, lowStartIndex, lowEndIndex);
			double midVal = AggregateValues(fftYVals, midStartIndex, midEndIndex);
			double highVal = AggregateValues(fftYVals, highStartIndex, highEndIndex);

			return new FrequencyBuckets
			{
				Low = lowVal,
				Mid = midVal,
				High = highVal,
			};
		}

		static double AggregateValues(double[] values, int startIndex, int endIndex)
		{
			double result = 0;

			for (int i = startIndex; i < endIndex; i++)
				result += values[i];

			return result / (endIndex - startIndex);
        }

		static int GetClosestNumberIndex(double[] values, int target)
		{
			if (values.Length <= 0)
				return -1;

			int closest = 0;
			double closestDifference = double.PositiveInfinity;

            for (int i = 0; i < values.Length; i++)
            {
				double difference = Math.Abs(values[i] - target);

				if (difference < closestDifference)
				{
					closest = i;
					closestDifference = difference;
				}
            }

            return closest;
		}

		/// <summary>
		/// Reset the graph to 'default' state
		/// </summary>
		private void ResetYAxisScaling(int val1 = -1, int val2 = -1)
		{
			if (this.autoScaleCheckBox.Checked == true)
			{
				if (val1 >= 0 && val2 >= 0)
				{
					fftPlot.Plot.Axes.SetLimits(
						0,
						20_000,
						0,
						Math.Max(val1, val2)
					);
				}
				else
				{
					fftPlot.Plot.Axes.SetLimits(
						0,
						20_000,
						0,
						60
					);
				}
			}

		}
	}
}
