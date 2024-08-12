namespace AudioToSerial
{
	using NAudio.Wave;
	using System.IO.Ports;
	using Timer = System.Windows.Forms.Timer;

	public partial class AudioApp : Form
	{
		private SerialPort? currentPort = null;
		private DesktopAudioCapture audioCapture;

		private int selectedDeviceIndex = -1;

		private Timer updateTimer;

		public AudioApp()
		{
			InitializeComponent();

			audioCapture = new DesktopAudioCapture();

			this.waveViewer.SamplesPerPixel = 5;

			updateTimer = new Timer();
			updateTimer.Interval = 100;
			updateTimer.Tick += UpdateTimer_Tick;
		}

		private void UpdateTimer_Tick(object? sender, EventArgs e)
		{
			byte[] buffer = audioCapture.GetBufferForWaveViewer();

			if (buffer.Length > 0)
			{
				using (var waveStream = new RawSourceWaveStream(buffer, 0, buffer.Length, audioCapture.WaveFormat))
				{
					this.waveViewer.WaveStream = waveStream;
					this.waveViewer.Refresh();
				}
			}
		}

		private void AudioApp_Load(object sender, EventArgs e)
		{
			Refresh_AudioOutputs();
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

		private void Refresh_AudioOutputs()
		{
			this.audioOutputCombo.Items.Clear();

			for (int n = -1; n < WaveOut.DeviceCount; n++)
			{
				string s = $"{(n == -1 ? "Default Device" : WaveOut.GetCapabilities(n).ProductName)}"; // indicate the default device
				this.audioOutputCombo.Items.Add(s);

			}

			this.audioOutputCombo.SelectedIndex = selectedDeviceIndex + 1;
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
			this.logBox.Items.Add(e);
		}

		private void refreshAudioButton_Click(object sender, EventArgs e)
		{
			Refresh_AudioOutputs();
		}

		private void audioOutputCombo_SelectionChangeCommitted(object sender, EventArgs e)
		{
			selectedDeviceIndex = ConvertListIndexToDeviceIndex(this.audioOutputCombo.SelectedIndex);

			try
			{

			}
			catch
			{
				Console.Error.WriteLine("Oops! Dev error.");
			}
		}

		private static int ConvertListIndexToDeviceIndex(int listIndex)
		{
			// the first (default device) is -1
			return listIndex - 1;
		}

		private void AudioApp_FormClosing(object sender, FormClosingEventArgs e)
		{
			audioCapture.StopCapture();
			updateTimer.Stop();
			updateTimer.Dispose();
		}
	}
}
