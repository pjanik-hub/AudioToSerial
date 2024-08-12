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

		public AudioApp()
		{
			InitializeComponent();

			audioCapture = new DesktopAudioCapture();

			this.waveViewer.SamplesPerPixel = 10;

			updateTimer = new Timer();
			updateTimer.Interval = 100;
			updateTimer.Tick += UpdateTimer_Tick;
		}

		private void UpdateTimer_Tick(object? sender, EventArgs e)
		{
			byte[] buffer = audioCapture.GetBufferForWaveViewer();
			this.FrequencyBuckets = audioCapture.GetFrequencyBuckets();

			if (buffer.Length > 0)
			{
				using (var waveStream = new RawSourceWaveStream(buffer, 0, buffer.Length, audioCapture.WaveFormat))
				{
					this.waveViewer.WaveStream = waveStream;
					this.waveViewer.Refresh();
				}
			}

			this.lowTextBox.Text = this.FrequencyBuckets.Low.ToString();
			this.midTextBox.Text = this.FrequencyBuckets.Mid.ToString();
			this.highTextBox.Text = this.FrequencyBuckets.High.ToString();
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
