namespace AudioToSerial
{
	using NAudio.Wave;
	using System.ComponentModel;
	using System.IO.Ports;
	using Timer = System.Windows.Forms.Timer;

	public partial class AudioApp : Form
	{
		private SerialPort? currentPort = null;
		private readonly DesktopAudioCapture audioCapture;
		private readonly Timer updateTimer;
		private readonly FrequencyBuckets FrequencyBuckets;
		private readonly FrequencyToSerial audioToPort;

		private const int SAMPLES_PER_PIXEL = 2;
		private const int FORM_UPDT_INTERVAL = 80;
		private const int BAUD_RATE = 14400;
		private const string STRING_FORMAT = "0.##E+0";

		// TODO: can remove min since we only use positive freqs
		private double minLowFreq = double.PositiveInfinity;
		private double maxLowFreq = double.NegativeInfinity;
		private double minMidFreq = double.PositiveInfinity;
		private double maxMidFreq = double.NegativeInfinity;
		private double minHighFreq = double.PositiveInfinity;
		private double maxHighFreq = double.NegativeInfinity;

		public AudioApp()
		{
			InitializeComponent();

			audioCapture = new DesktopAudioCapture();

			this.waveViewer.SamplesPerPixel = SAMPLES_PER_PIXEL;

			updateTimer = new Timer();
			updateTimer.Interval = FORM_UPDT_INTERVAL;
			updateTimer.Tick += UpdateTimer_Tick;

			FrequencyBuckets = new FrequencyBuckets();
			audioToPort = new FrequencyToSerial();
			audioToPort.ConnectPort("COM6", 9600); // using COM6 with serial cable
		}

		private void UpdateTimer_Tick(object? sender, EventArgs e)
		{
			try
			{
				byte[] buffer = audioCapture.GetBufferForWaveViewer();

				if (buffer.Length > 0)
				{
					FrequencyBuckets freq = audioCapture.GetFrequencyBuckets();
					UpdateFrequencyAmplitudes(freq);
					audioToPort.SendData(freq);

					Console.WriteLine($"Data out: low={freq.Low},mid={freq.Mid},high={freq.High}");

					using (var waveStream = new RawSourceWaveStream(buffer, 0, buffer.Length, audioCapture.WaveFormat))
					{
						this.waveViewer.WaveStream = waveStream;
						this.waveViewer.Refresh();
					}
				}
			}
			catch
			{
				Console.Error.WriteLine("Bad things happened!");
			}
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

			updateTimer.Start();
			audioCapture.Start();
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
		}

		private void AudioApp_FormClosing(object sender, FormClosingEventArgs e)
		{
			audioCapture.StopCapture();
			updateTimer.Stop();
			updateTimer.Dispose();
		}
	}
}
