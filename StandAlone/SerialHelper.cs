using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone
{
    class SerialHelper : SerialPort
    {
        SerialPort _sp;

        public SerialPort Create(int BaudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            // config is from https://www.cloudynights.com/topic/217730-connecting-lx200-to-computer/?p=2793705
            _sp.PortName = AppSettings.Default.SerialPort;
            _sp.BaudRate = BaudRate;
            _sp.Parity = parity;
            _sp.DataBits = dataBits;
            _sp.StopBits = StopBits;

            return _sp;
        }

    }
}
