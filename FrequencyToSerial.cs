namespace AudioToSerial
{
	using System.IO.Ports;
	using Newtonsoft.Json;

	public class FrequencyToSerial
	{
		public FrequencyToSerial() { }

		public SerialPort? currentPort { get; private set; }

		public void ConnectPort(string portName, int baudRate)
		{
			SerialPort port = new(portName, baudRate, Parity.None);
			port.Handshake = Handshake.None;

			this.currentPort = port;
			ClosePort();
			this.currentPort.Open();
		}

		public void SendData(FrequencyBuckets bucket)
		{
			if (this.currentPort == null || this.currentPort.IsOpen == false)
				return;

			string json = SerializeFrequencies(bucket);
			this.currentPort.WriteLine(json);
		}

		public void ClosePort()
		{
			if (this.currentPort != null && this.currentPort.IsOpen)
			{
				this.currentPort.Close();
			}
		}

		private static string SerializeFrequencies(FrequencyBuckets bucket)
		{
			string json = JsonConvert.SerializeObject(bucket);

			return json;
		}
	}
}
