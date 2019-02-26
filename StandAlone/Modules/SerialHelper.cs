using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StandAlone.Modules
{
    /// <summary>
    /// https://www.meade.com/support/LX200CommandSet.pdf
    /// </summary>
    public partial class SerialHelper : SerialPort
    {
        /// <summary>
        /// The data buffer, automatically updating when receiving new data.
        /// </summary>
        public string AvailableData {
            get { return AvailableData; }

            internal set {
                if (AvailableData != value)
                    AvailableData = value;
            }
        }

        /// <summary>
        /// The buffer size (in bytes) for serial reading.
        /// </summary>
        private const int CReadBufferSize = 1024;

        /// <summary>
        /// The encoding to be used during telescope communications.
        /// </summary>
        private Encoding PEncoding { get; set; }

        /// <summary>
        /// The <see cref="SerialPort"/> to be used for communications.
        /// </summary>
        private SerialPort _serialPort;

        /// <summary>
        /// Initializes a new instance of the SerialHelper class.
        /// </summary>
        /// <param name="PortName">A port name, such as "COM4".</param>
        /// <param name="BaudRate">A byte update rate (the usual and most supported value is 9600).</param>
        /// <param name="Parity">A parity configuration.</param>
        /// <param name="StopBits">The number of stop bits.</param>
        /// <param name="DataBits">The number of data bits.</param>
        /// <param name="Encoding">The encoding method (the usual method is ASCII).</param>
        public SerialHelper(string PortName, int BaudRate, Parity Parity, StopBits StopBits, int DataBits, Encoding Encoding)
        {
            _serialPort = new SerialPort()
            {
                PortName = PortName,
                BaudRate = BaudRate,
                Parity = Parity,
                StopBits = StopBits,
                DataBits = DataBits,
                ReadTimeout = -1,
                WriteTimeout = -1,
                ReadBufferSize = 1024
            };

            PEncoding = Encoding;

            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();
            }
            catch (Exception ex)
            {
                LogHelper.WriteS("Serial port error: " + ex.Message, "SERIAL", LogHelper.MessageTypes.ERROR);
                Program.OnProgramComplete();
            }

        }

        /// <summary>
        /// If the telescope not available to receive commands.
        /// </summary>
        private bool ScopeBusy;

        /// <summary>
        /// Sends a command to the end unit.
        /// </summary>
        /// <param name="Command">Command text.</param>
        /// <param name="RequestedDomeHeading">The requested dome heading in degrees. -1 means no motion required.</param>
        /// <returns></returns>
        //public string DoCommand(string Command, int RequestedDomeHeading = -1)
        //{
        //    byte[] buffer = new byte[CReadBufferSize];
        //    SerialQueue<string> commandQueue = new SerialQueue<string>();

        //    _serialPort.Write("C[" + Command + "]D[" + RequestedDomeHeading.ToString() + "]"); // write command and requested dome heading.

        //    Thread.Sleep(10);
        //    // check for the result of the command.
        //    // if we receive NAK (0x15), the telescope is busy, otherwise we should receive atleast
        //    // 1 byte of a result.

        //    _serialPort.Read(buffer, 0, CReadBufferSize);

        //    // Register scope status:
        //    ScopeBusy = buffer.Contains<byte>(0x15);

        //    // Convert the input bytes to a string.
        //    string output = _serialPort.Encoding.GetString(buffer);

        //    if (commandQueue.TryCommand(Command) == false)
        //    { // currently busy, add command to queue.
        //        commandQueue.Add(Command);
        //    }

        //    return output;
        //}

        bool dataReceived = false;
        
        public string DoCommand(string cmd)
        {
            byte[] buffer = new byte[CReadBufferSize];
            SerialQueue<string> commandQueue = new SerialQueue<string>();

            try
            {
                _serialPort.Write(cmd);

                Console.Write("UP: ");
                foreach (byte b in Encoding.GetBytes(cmd))
                {
                    Console.Write(b.ToString().PadLeft(3, ' '));
                }
                Console.WriteLine();

                Thread.Sleep(5);
            }
            catch (Exception ex)
            {
                LogHelper.WriteS("Error serial writing: " + ex.Message, "SERIAL", LogHelper.MessageTypes.ERROR);
                return null;
            }

            Thread.Sleep(10);

            //string output = "";
            //int i = 0;
            //do
            //{
            //    _serialPort.Read(buffer, 0, 1024);
            //    output += _serialPort.Encoding.GetString(buffer); // Convert the input bytes to a string.
            //    buffer = new byte[CReadBufferSize];

            //    foreach (byte c in Encoding.GetBytes(output))
            //    {
            //        Console.Write(c + "\t");
            //    }

            //} while (!output.Contains("#"));

            string output = _serialPort.ReadTo("#");

            Console.Write("DN: ");
            foreach (byte b in Encoding.GetBytes(output))
            {
                Console.Write(b.ToString().PadLeft(3, ' '));
            }
            Console.WriteLine();

            return output;
        }

        public bool IsScopeBusy()
        {
            return ScopeBusy;
        }
    }
}
