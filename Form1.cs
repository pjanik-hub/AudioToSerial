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
		const string STRING_FORMAT = "0.##E+0";

		const int Sample_Rate = 44100;
		const int Buffer_MS = 100;
		readonly TimeSpan Buffer_Length = new(0, 0, 0, 0, Buffer_MS);

		readonly WasapiLoopbackCapture Audio_Capture;
		readonly double[] Audio_Data;
		readonly double[] FFT_Data;
		readonly int Audio_Data_Length = Sample_Rate * Buffer_MS / 1000;
		readonly int FFT_Data_Length;

		// TODO: can remove min since we only use positive freqs
		double minLowFreq = double.PositiveInfinity;
		double maxLowFreq = double.NegativeInfinity;
		double minMidFreq = double.PositiveInfinity;
		double maxMidFreq = double.NegativeInfinity;
		double minHighFreq = double.PositiveInfinity;
		double maxHighFreq = double.NegativeInfinity;

		public AudioApp()
		{
			InitializeComponent();

			Audio_Data = new double[Audio_Data_Length];

			double[] paddedAudio = FftSharp.Pad.ZeroPad(Audio_Data);
			Complex[] complexFft = FftSharp.FFT.Forward(paddedAudio);
			double[] fftAmplitudes = FftSharp.FFT.Magnitude(complexFft);

			FFT_Data_Length = fftAmplitudes.Length;
			FFT_Data = new double[fftAmplitudes.Length];

			double fftPeriod = FftSharp.Transform.FFTfreqPeriod(Sample_Rate, fftAmplitudes.Length);

			fftPlot.Plot.Add.Signal(FFT_Data, 1 / fftPeriod);
			fftPlot.Plot.XLabel("PWR");
			fftPlot.Plot.XLabel("Freq. (kHz)");
			fftPlot.Refresh();

			this.Audio_Capture = new WasapiLoopbackCapture()
			{
				WaveFormat = new WaveFormat(Sample_Rate, 16, 1),
			};
			this.Audio_Capture.DataAvailable += Audio_Capture_DataAvailable;

			FrequencyBuckets = new FrequencyBuckets();
			audioToPort = new FrequencyToSerial();

			timer1.Interval = Buffer_MS;
		}

		private void Audio_Capture_DataAvailable(object? sender, WaveInEventArgs e)
		{
			int sampleCount = e.Buffer.Length / 2;

			for (int i = 0; i < Audio_Data_Length && i < sampleCount; i++)
				Audio_Data[i] = BitConverter.ToInt16(e.Buffer, i * 2);
		}

		private void UpdateFrequencyAmplitudes(FrequencyBuckets frequencyBuckets)
		{
			this.lowTextBox.Text = GetFriendlyTextFromFreq(frequencyBuckets.Low);
			this.midTextBox.Text = GetFriendlyTextFromFreq(frequencyBuckets.Mid);
			this.highTextBox.Text = GetFriendlyTextFromFreq(frequencyBuckets.High);

			if (frequencyBuckets.Low < minLowFreq)
			{
				minLowFreq = frequencyBuckets.Low;
				this.minLowTxtBox.Text = GetFriendlyTextFromFreq(minLowFreq);
			}
			if (frequencyBuckets.Low > maxLowFreq)
			{
				maxLowFreq = frequencyBuckets.Low;
				this.maxLowTxtBox.Text = GetFriendlyTextFromFreq(maxLowFreq);
			}

			if (frequencyBuckets.Mid < minMidFreq)
			{
				minMidFreq = frequencyBuckets.Mid;
				this.minMidTxtBox.Text = GetFriendlyTextFromFreq(minMidFreq);
			}
			if (frequencyBuckets.Mid > maxMidFreq)
			{
				maxMidFreq = frequencyBuckets.Mid;
				this.maxMidTxtBox.Text = GetFriendlyTextFromFreq(maxMidFreq);
			}

			if (frequencyBuckets.High < minHighFreq)
			{
				minHighFreq = frequencyBuckets.High;
				this.minHighTxtBox.Text = GetFriendlyTextFromFreq(minHighFreq);
			}
			if (frequencyBuckets.High > maxHighFreq)
			{
				maxHighFreq = frequencyBuckets.High;
				this.maxHighTxtBox.Text = GetFriendlyTextFromFreq(maxHighFreq);
			}
		}

		private string GetFriendlyTextFromFreq(double input)
		{
			return input.ToString(STRING_FORMAT);
		}

		private void AudioApp_Load(object sender, EventArgs e)
		{
			Refresh_SerialPorts();

			EmptyFillAudioData();
			Audio_Capture.StartRecording();

			timer1.Start();
		}

		private void EmptyFillAudioData()
		{
			for (int i = 0; i < Audio_Data_Length; i++)
				Audio_Data[i] = 0;
		}

		private void Refresh_SerialPorts()
		{
			string[] ports = SerialPort.GetPortNames();

			serialsCombo.Items.Clear();
			serialsCombo.Items.AddRange(ports);
		}

		private void refreshSerialButton_Click(object sender, EventArgs e)
		{
			Refresh_SerialPorts();
		}

		private void bindButton_Click(object sender, EventArgs e)
		{
			string? portName = (string?)serialsCombo.SelectedItem;

			if (portName == null)
				return;

			audioToPort.ConnectPort(portName, BAUD_RATE); // using COM6 with serial cable
		}

		private void AudioApp_FormClosing(object sender, FormClosingEventArgs e)
		{
			Audio_Capture.StopRecording();
			Audio_Capture.Dispose();
			timer1.Stop();
			timer1.Dispose();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			double[] paddedAudio = FftSharp.Pad.ZeroPad(Audio_Data);
			Complex[] complexFft = FftSharp.FFT.Forward(paddedAudio);
			double[] fftAmplitudes = FftSharp.FFT.Magnitude(complexFft);

			Array.Copy(fftAmplitudes, FFT_Data, fftAmplitudes.Length);

			double fftPeakAmp = fftAmplitudes.Max();
			double plotYMax = fftPlot.Plot.Axes.GetLimits().Top;

			fftPlot.Plot.Axes.SetLimits(
				0,
				20,
				0,
				Math.Max(fftPeakAmp, plotYMax)
			);

			// fftPlot.Plot.Add.Signal(FFT_Data);
			fftPlot.Refresh();
		}
	}
}
