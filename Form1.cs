using NAudio.Wave;
using System.IO.Ports;

namespace AudioToSerial
{
	public partial class AudioApp : Form
	{
		private SerialPort? currentPort;
		private WaveOutEvent currentWaveOut;

		public AudioApp()
		{
			InitializeComponent();
			currentPort = null;
			currentWaveOut = new WaveOutEvent() { DeviceNumber = -1 };
			Refresh_AudioOutputs();
		}

		private void AudioApp_Load(object sender, EventArgs e)
		{
			Refresh_SerialPorts();
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
				string s = $"{WaveOut.GetCapabilities(n).ProductName}" +
					$"{(n == -1 ? " (Default)" : "")}"; // indicate the default device
				this.audioOutputCombo.Items.Add(s);

				// ensure to change the selected device (i.e. first in Devices)
				if (n == -1)
					this.audioOutputCombo.SelectedIndex = 0;
			}


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
	}
}
