namespace AudioToSerial
{
	using NAudio.Wave;
	using System.IO.Ports;
	using System.Numerics;

	public partial class AudioApp : Form
	{
		SerialPort? currentPort = null;
		readonly FrequencyBuckets FrequencyBuckets;
		readonly FrequencyToSerial audioToPort;

		const int BAUD_RATE = 9600;

		const int Sample_Rate = 44100;
		const int Buffer_MS = 80;

		readonly WasapiLoopbackCapture Audio_Capture;
		readonly double[] Audio_Data;
		readonly double[] FFT_Data;
		readonly double[] FFT_Freqs;
		readonly int Audio_Data_Length = Sample_Rate * Buffer_MS / 1000;
		readonly int FFT_Data_Length;
		readonly int FFT_Freqs_Length;

		public AudioApp()
		{
			InitializeComponent();

			Audio_Data = new double[Audio_Data_Length];

			double[] paddedAudio = FftSharp.Pad.ZeroPad(Audio_Data);
			Complex[] complexData = FftSharp.FFT.Forward(paddedAudio);

			double[] fftAmplitudes = FftSharp.FFT.Power(complexData);
			double[] fftFrequencyScale = FftSharp.FFT.FrequencyScale(fftAmplitudes.Length, Sample_Rate);

			FFT_Data_Length = fftAmplitudes.Length;
			FFT_Data = new double[fftAmplitudes.Length];

			FFT_Freqs_Length = fftFrequencyScale.Length;
			FFT_Freqs = new double[fftFrequencyScale.Length];

			fftPlot.Plot.Add.SignalXY(FFT_Freqs, FFT_Data);
			fftPlot.Plot.YLabel("PWR (RMS)");
			fftPlot.Plot.XLabel("Freq. (kHz)");
			fftPlot.Refresh();

			this.Audio_Capture = new WasapiLoopbackCapture()
			{
				WaveFormat = new WaveFormat(Sample_Rate, 16, 1),
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
				var window = new FftSharp.Windows.Hanning();
				window.ApplyInPlace(Audio_Data);

				double[] paddedAudio = FftSharp.Pad.ZeroPad(Audio_Data);
				Complex[] complexData = FftSharp.FFT.Forward(paddedAudio);

				double[] fftAmplitudes = this.powerCheckBox.Checked ?
					FftSharp.FFT.Power(complexData) :    // dB
					FftSharp.FFT.Magnitude(complexData); // RMS
				double[] fftFrequencyScale = FftSharp.FFT.FrequencyScale(fftAmplitudes.Length, Sample_Rate);

				Array.Copy(fftAmplitudes, FFT_Data, FFT_Data_Length);
				Array.Copy(fftFrequencyScale, FFT_Freqs, FFT_Freqs_Length);

				double fftPeakAmp = fftAmplitudes.Max();
				double plotYMax = fftPlot.Plot.Axes.GetLimits().Top;
				fftPlot.Plot.Axes.SetLimits(
					0,
					20_000,
					0,
					Math.Min(Math.Max(fftPeakAmp, plotYMax), 120) // max at 120 PWR
				);

				// fftPlot.Plot.Add.Signal(FFT_Data);
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
		}
	}
}
