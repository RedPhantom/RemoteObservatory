using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StandAlone.TelescopeDictionary
{
    /// <summary>
    /// https://www.meade.com/support/LX200CommandSet.pdf
    /// </summary>
    class SerialHelper : SerialPort
    {
        private const int CReadBufferSize = 1024;
        private Encoding PEncoding { get; set; }

        SerialHelper New(string PortName, int BaudRate, Parity Parity, StopBits StopBits, int DataBits, Encoding Encoding)
        {
            SerialHelper _serialHelper = new SerialHelper()
            {
                PortName = PortName,
                BaudRate = BaudRate,
                Parity = Parity,
                StopBits = StopBits,
                DataBits = DataBits,
                ReadTimeout = 500,
                WriteTimeout = 500,
                ReadBufferSize = 1024
            };
            PEncoding = Encoding;
            _serialHelper.Open();

            return _serialHelper;
        }

        public string DoCommand(string Command)
        {
            byte[] buffer = new byte[CReadBufferSize];

            this.Write(Command);
            System.Threading.Thread.Sleep(10);
            this.Read(buffer, 0, CReadBufferSize);
            string output = this.Encoding.GetString(buffer);
            return output;
        }
    }
}
