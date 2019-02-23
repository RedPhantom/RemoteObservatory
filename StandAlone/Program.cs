using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using StandAlone.TelescopeDictionary;
using StandAlone.Modules;
using System.ComponentModel;
using StandAlone.TCP;

namespace StandAlone
{
    class Program
    {
        // CONSTANTS

        const int InitialBaudRate = 9600;

        // MODELS
        public static NexStar_Generic scope;
        public static SerialHelper telescopeSerialHelper; // speaks with the telescope.
        public static SerialHelper endUnitSerialHelper;   // speaks with the arduino end unit which speaks with the dome.
        public static AppSettings appSettings;

        public static List<ConnectionState> Clients { get; set; }

        public static TcpServer server;

        public static void Main()
        {
            // Initialize system components.
            LogHelper.WriteS("Loading server settings...", "INIT", LogHelper.messageTypes.INFO);
            SettingsInit();
            LogHelper.WriteS("Server settings loaded.", "INIT", LogHelper.messageTypes.INFO, true, true);

            // TCP SERVER
            /*
             * The TCP-server allows us to display all of the scope information on a separate application
             * that receives the data via TCP connection.
             */
            LogHelper.WriteS("Starting internal control server... ", "SERVER", LogHelper.messageTypes.INFO);
            RCProvider provider = new RCProvider();
            server = new TcpServer(provider, 15100);
            server.Start();
            LogHelper.WriteS("Internal control server started and is awating clients.", "SERVER", LogHelper.messageTypes.INFO);

            // Serials:
            LogHelper.WriteS("Initializing the SerialHelper module... ", "SERIAL", LogHelper.messageTypes.INFO);
            SerialInit();
            LogHelper.WriteS("Serial helper connected successfully.", "SERIAL", LogHelper.messageTypes.INFO);

            LogHelper.WriteS("Initializing the scope... ", "SCOPE", LogHelper.messageTypes.INFO);
            ScopeInit();
            LogHelper.WriteS("Scope serial helper connected successfully.", "SCOPE", LogHelper.messageTypes.INFO);

            // OBJECTS


            // MODULES


            // SERVICES

            // SERIAL SERVICE
            LogHelper.WriteS("Starting the main communication service...", "SERIVCES", LogHelper.messageTypes.INFO, false);
            BackgroundWorker SerialService = new BackgroundWorker();
            SerialService.DoWork += SerialServiceStart;
            SerialService.RunWorkerAsync();
            LogHelper.WriteS("Main service running.", "", LogHelper.messageTypes.INFO, true, true);

            TestRoutine();
            OnProgramComplete();
        }

        public static void OnProgramComplete()
        {
            Console.Write("Program complete. Press any key to exit . . .");
            Console.ReadKey();
        }

        private static void SerialServiceStart(object sender, DoWorkEventArgs e)
        {
            
        }

        public static void SerialInit()
        {
            // telescope:
            Parity parity = NexStar_Generic.CPairity;
            StopBits stopBits = NexStar_Generic.CStopBits;
            int dataBits = NexStar_Generic.DataBits;

            telescopeSerialHelper = new SerialHelper(appSettings.TelescopeSerialPort, InitialBaudRate, parity, stopBits, dataBits, Encoding.ASCII);
            telescopeSerialHelper.DataReceived += telescopeSerialHelper.HandleDataIn; // get the data when it is received.

            // end unit:
            //endUnitSerialHelper = new SerialHelper("COM9", 9600, Parity.None, StopBits.One, 8, Encoding.ASCII);
        }

        public static void ScopeInit()
        {
            //telescope = new MeadeLX200_16GPS(telescopeSerialHelper, endUnitSerialHelper);
            scope = new NexStar_Generic(telescopeSerialHelper, endUnitSerialHelper);
        }

        public static void SettingsInit()
        {
            appSettings = new AppSettings();
        }

        /// <summary>
        /// A continues loop for sending commands to the telescope.
        /// </summary>
        static void TestRoutine()
        {
            /* Testing tasks:
             * 1. get alignment mode.
             * 2. navigate to certain corrdinates.
             * 3. perform object tracking.
             */
            Console.WriteLine(scope.GetTrackingMode().ToString());

        }

    }
}
