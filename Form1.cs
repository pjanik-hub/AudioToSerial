namespace AudioToSerial
{
	using NAudio.Wave;
	using System.IO.Ports;
	using Timer = System.Windows.Forms.Timer;

	public partial class AudioApp : Form
	{
		private SerialPort? currentPort = null;
		private readonly DesktopAudioCapture audioCapture;
		private readonly Timer updateTimer;
		private FrequencyBuckets FrequencyBuckets;

		private double minLowFreq = 0;
		private double maxLowFreq = 0;
		private double minMidFreq = 0;
		private double maxMidFreq = 0;
		private double minHighFreq = 0;
		private double maxHighFreq = 0;

		public AudioApp()
		{
			InitializeComponent();

			audioCapture = new DesktopAudioCapture();

			this.waveViewer.SamplesPerPixel = 10;

			updateTimer = new Timer();
			updateTimer.Interval = 100;
			updateTimer.Tick += UpdateTimer_Tick;

			FrequencyBuckets = new FrequencyBuckets();
		}

		private void UpdateTimer_Tick(object? sender, EventArgs e)
		{
			byte[] buffer = audioCapture.GetBufferForWaveViewer();
			FrequencyBuckets freq = audioCapture.GetFrequencyBuckets();

			if (buffer.Length > 0)
			{
				using (var waveStream = new RawSourceWaveStream(buffer, 0, buffer.Length, audioCapture.WaveFormat))
				{
					this.waveViewer.WaveStream = waveStream;
					this.waveViewer.Refresh();
				}
			}

			UpdateFrequencyAmplitudes(freq);
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
			return input.ToString("0.##E+0");
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

			this.currentPort = new SerialPort(portName, 14400);
			this.currentPort.DataReceived += SerialPort_RT;
		}

		private void SerialPort_RT(object sender, EventArgs e)
		{
			
		}

		private void AudioApp_FormClosing(object sender, FormClosingEventArgs e)
		{
			audioCapture.StopCapture();
			updateTimer.Stop();
			updateTimer.Dispose();
		}
	}
}
