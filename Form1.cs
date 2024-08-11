using NAudio.Wave;
using System;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace AudioToSerial
{
	public partial class AudioApp : Form
	{
		private SerialPort? currentPort;
		private WaveOut currentWaveOut;
		private WaveStream currentStream;

		private int selectedDeviceIndex = -1;

		private readonly string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
		private readonly string outputPath;

		WasapiLoopbackCapture capture;
		private BufferedWaveProvider buffer;

		public AudioApp()
		{
			InitializeComponent();

			if (!Directory.Exists(outputFolder))
				Directory.CreateDirectory(outputFolder);

			outputPath = Path.Combine(outputFolder, "audio.wav");

			capture = new WasapiLoopbackCapture();
			buffer = new BufferedWaveProvider(capture.WaveFormat);

			capture.DataAvailable += Capture_DataAvailable;

			currentPort = null;
			currentWaveOut = new WaveOut() { DeviceNumber = -1 };
		}

		private void Capture_DataAvailable(object? sender, WaveInEventArgs e)
		{
			buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
		}

		private void AudioApp_Load(object sender, EventArgs e)
		{
			Refresh_AudioOutputs();
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
				currentWaveOut.DeviceNumber  = selectedDeviceIndex;

				waveViewer.WaveStream = currentStream;
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
	}
}
