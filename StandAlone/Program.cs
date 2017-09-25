using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace StandAlone
{
    class Program
    {
        static void Main(string[] args)
        {

            SerialHelper serialPort = new SerialHelper();
            serialPort.Create();

        }


    }
}
