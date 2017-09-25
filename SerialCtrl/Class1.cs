using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SerialCtrl
{
    public class SerialCtrl
    {
        /// <summary>
        /// Bit rate for serial.
        /// </summary>
        public int BaudRate;

        /// <summary>
        /// Port name.
        /// </summary>
        public string PortName;

        public SerialPort serialPort;
    
    }
}
