namespace AudioToSerial
{
	using System.IO.Ports;
	using Newtonsoft.Json;

	/// <summary>
	/// Small wrapper to send freq. data to COM port
	/// </summary>
	public class FrequencyToSerial
	{
		public FrequencyToSerial() 
		{
			CurrentPort = null;
			IsConnected = false;
		}

		public SerialPort? CurrentPort { get; private set; }
		public bool IsConnected { get; private set; }

		public void ConnectPort(string portName, int baudRate)
		{
			SerialPort port = new(portName, baudRate, Parity.None);
			port.Handshake = Handshake.None;

			ClosePort();
			this.CurrentPort = port;
			this.CurrentPort.Open();

			IsConnected = true;
		}

		public void SendData(FrequencyBuckets bucket)
		{
			if (this.CurrentPort == null || this.CurrentPort.IsOpen == false)
				return;

			string json = SerializeFrequencies(bucket);
			this.CurrentPort.WriteLine(json);
		}

		public void ClosePort()
		{
			if (this.CurrentPort != null && this.CurrentPort.IsOpen)
			{
				this.CurrentPort.Close();
				IsConnected = false;
			}
		}

		private static string SerializeFrequencies(FrequencyBuckets bucket)
		{
			string json = JsonConvert.SerializeObject(bucket);

			return json;
		}
	}
}
