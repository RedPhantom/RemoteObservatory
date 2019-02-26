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
using ZWOptical.ASISDK.ObjectModel;

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
            //#if (!DEBUG)
            StartupScreen();
            //#endif
            // Initialize system components.
            LogHelper.WriteS("Loading server settings...", "INIT", LogHelper.MessageTypes.INFO);
            SettingsInit();
            LogHelper.WriteS("Server settings loaded.", "INIT", LogHelper.MessageTypes.SUCCESS);

            // TCP SERVER
            /*
             * The TCP-server allows us to display all of the scope information on a separate application
             * that receives the data via TCP connection.
             */
            LogHelper.WriteS("Starting internal control server...", "SERVER", LogHelper.MessageTypes.INFO);
            RCProvider provider = new RCProvider();
            server = new TcpServer(provider, 15100);
            server.Start();
            LogHelper.WriteS("Internal control server started on 127.0.0.1:15101 and is awating clients.", "SERVER", LogHelper.MessageTypes.SUCCESS);

            // Serials:
            //LogHelper.WriteS("Initializing the dome controller module...", "SERIAL", LogHelper.MessageTypes.INFO);
            //SerialDomeInit();
            //LogHelper.WriteS("Dome controller connected successfully.", "SERIAL", LogHelper.MessageTypes.SUCCESS);

            LogHelper.WriteS("Initializing the scope...", "SCOPE", LogHelper.MessageTypes.INFO);
            SerialScopeInit();
            LogHelper.WriteS("Scope settings loaded.", "SCOPE", LogHelper.MessageTypes.SUCCESS);
            ScopeInit();
            LogHelper.WriteS("Scope serial connection successful.", "SCOPE", LogHelper.MessageTypes.SUCCESS);

            // CAMERAS


            // MODULES


            // SERVICES
            LogHelper.WriteS("Starting the main service...", "SERIVCE", LogHelper.MessageTypes.INFO);
            BackgroundWorker MainService = new BackgroundWorker();
            MainService.DoWork += ServiceStart;
            MainService.RunWorkerAsync();
            LogHelper.WriteS("Main service running.", "SERIVCE", LogHelper.MessageTypes.INFO);

            while (Console.ReadLine() != "f")
            {
                TestRoutine();
                System.Threading.Thread.Sleep(5000);
            }
            OnProgramComplete();
        }

        public static void OnProgramComplete()
        {
            Shutdown();
            Console.Write("Execution endpoint reached. Press any key to exit. . .");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void ServiceStart(object sender, DoWorkEventArgs e)
        {
            // starts the service that executes commands to perform required tasks.

        }

        public static void SerialScopeInit()
        {
            // telescope:
            Parity parity = NexStar_Generic.CPairity;
            StopBits stopBits = NexStar_Generic.CStopBits;
            int dataBits = NexStar_Generic.DataBits;

            // todo: load from config
            // list available ports:
            string[] ports = SerialPort.GetPortNames();
            string com = "";

            if (ports.Length > 1)
            {
                Console.Write("Available ports: ");
                foreach (string name in SerialPort.GetPortNames())
                {
                    Console.Write(name + " ");
                };
                Console.WriteLine();

                Console.Write("Select COM Port #: COM");
                com = Console.ReadLine();
            }
            else if (ports.Length == 1)
                com = SerialPort.GetPortNames()[0].Substring(3);
            else
            {
                LogHelper.WriteS("No available COM ports.", "ERROR", LogHelper.MessageTypes.ERROR);
                OnProgramComplete();
            }

            telescopeSerialHelper = new SerialHelper("COM" + com, InitialBaudRate, parity, stopBits, dataBits, Encoding.ASCII);

            // end unit:
            //endUnitSerialHelper = new SerialHelper("COM9", 9600, Parity.None, StopBits.One, 8, Encoding.ASCII);
        }

        public static void SerialDomeInit()
        {

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

            // testing routine for the camera.

            LogHelper.WriteS("Starting demo program 'scope slewing'.", "DEMO", LogHelper.MessageTypes.INFO);
            LogHelper.WriteS("We cannot align the scope at daytime so we are", "DEMO", LogHelper.MessageTypes.INFO);
            LogHelper.WriteS("showing a demo that doesn't require alignment.", "DEMO", LogHelper.MessageTypes.INFO);
            LogHelper.WriteS("In this demo, the server parses a request filed", "DEMO", LogHelper.MessageTypes.INFO);
            LogHelper.WriteS("by a user to perform an observation on an object.", "DEMO", LogHelper.MessageTypes.INFO);
            LogHelper.WriteS("In a real situation, the system will command", "DEMO", LogHelper.MessageTypes.INFO);
            LogHelper.WriteS("the telescope to move to a specific object", "DEMO", LogHelper.MessageTypes.INFO);
            LogHelper.WriteS("and the telescope will do so automatically.", "DEMO", LogHelper.MessageTypes.INFO);

            Demo3();
        }

        static void Demo3()
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            byte s = 7;

            while (true)
            {
                if (cki.Key == ConsoleKey.A)
                    scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.AZM_Controller, s);

                if (cki.Key == ConsoleKey.D)
                    scope.StartSlewing(NexStar_Generic.SlewDirections.POS, NexStar_Generic.DeviceIDs.AZM_Controller, s);

                if (cki.Key == ConsoleKey.W)
                    scope.StartSlewing(NexStar_Generic.SlewDirections.POS, NexStar_Generic.DeviceIDs.ALT_Controller, s);

                if (cki.Key == ConsoleKey.S)
                    scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.ALT_Controller, s);

                if ((int)cki.Key >= 47 && (int)cki.Key <= 59)
                {
                    s = (byte)cki.Key;
                    s -= 46;
                    Console.WriteLine("Speed set to " + s);
                }

                if (cki.Key == ConsoleKey.Spacebar)
                {
                    scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.ALT_Controller, 0);
                    scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.AZM_Controller, 0);
                }

                cki = Console.ReadKey();
            }
        }

        static void Demo2()
        {
            byte s = 7;
            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.ALT_Controller, s);
            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.AZM_Controller, s);
            System.Threading.Thread.Sleep(1000);
            scope.StartSlewing(NexStar_Generic.SlewDirections.POS, NexStar_Generic.DeviceIDs.ALT_Controller, s);
            scope.StartSlewing(NexStar_Generic.SlewDirections.POS, NexStar_Generic.DeviceIDs.AZM_Controller, s);
            System.Threading.Thread.Sleep(1000);
            scope.StartSlewing(NexStar_Generic.SlewDirections.POS, NexStar_Generic.DeviceIDs.ALT_Controller, s);
            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.AZM_Controller, s);
            System.Threading.Thread.Sleep(1000);
            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.ALT_Controller, s);
            scope.StartSlewing(NexStar_Generic.SlewDirections.POS, NexStar_Generic.DeviceIDs.AZM_Controller, s);
            System.Threading.Thread.Sleep(1000);
            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.ALT_Controller, 0);
            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.AZM_Controller, 0); scope.StartSlewing(NexStar_Generic.SlewDirections.POS, NexStar_Generic.DeviceIDs.AZM_Controller, 2);
        }
        static void Demo1()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Random r = new Random();
            int controller = r.Next(0, 1);

            NexStar_Generic.SlewDirections sdr = r.Next(0, 2) == 1 ? NexStar_Generic.SlewDirections.NEG : NexStar_Generic.SlewDirections.POS;

            scope.StartSlewing(sdr, NexStar_Generic.DeviceIDs.ALT_Controller, 9);
            scope.StartSlewing(sdr, NexStar_Generic.DeviceIDs.AZM_Controller, 9);

            System.Threading.Thread.Sleep(r.Next(1000, 5000));

            scope.StartSlewing(sdr, NexStar_Generic.DeviceIDs.ALT_Controller, 7);
            scope.StartSlewing(sdr, NexStar_Generic.DeviceIDs.AZM_Controller, 7);

            System.Threading.Thread.Sleep(r.Next(1000, 2000));

            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.ALT_Controller, 0);
            scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.AZM_Controller, 0);

            LogHelper.WriteS("Destination reached.", "DEMO", LogHelper.MessageTypes.SUCCESS);
        }
        static void Shutdown()
        {
            // Stop the scope from slewing.
            if (scope != null)
            {
                scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.AZM_Controller, 0);
                scope.StartSlewing(NexStar_Generic.SlewDirections.NEG, NexStar_Generic.DeviceIDs.ALT_Controller, 0);
            }
        }

        // ---------------------------------- cosmetics ----------------------------------
        static void StartupScreen()
        {
            Console.CursorVisible = false;
            int width, height;
            int[] y;
            int[] l;
            Initialize(out width, out height, out y, out l);
            int ms;

            int steps = 20;
            int j = 0;

            while (j <= steps)
            {
                DateTime t1 = DateTime.Now;
                MatrixStep(width, height, y, l);
                ms = 10 - (int)((TimeSpan)(DateTime.Now - t1)).TotalMilliseconds;

                if (ms > 0)
                    System.Threading.Thread.Sleep(ms);

                if (Console.KeyAvailable)
                    if (Console.ReadKey().Key == ConsoleKey.F5)
                        Initialize(out width, out height, out y, out l);
                j++;
            }





            string s = @"

.          *         _________ __           .        ________                            .          
                    /   _____//  |______ _______    /  _____/_____  ________.____               .   
      .             \_____  \\   __\__  \\_  __ \  /   \  ___\__  \ \___   // __ \           .      
                 .  /        \|  |  / __ \|  | \/  \    \_\  \/ __ \_/    /\  ___/                  
      .             /_______  /|__| (____  /__|    . \______  (____  /_____ \\___  >     .          
                            \/           \/      .          \/     \/      \/    \/               . 
          .       *                                                        .                        
              .              .   Populating science, worldwide | open source         *              ";

            int cursorTop = 3;
            int cursorLeft = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
                System.Threading.Thread.Sleep(1);
            }

            System.Threading.Thread.Sleep(2750);



            Console.ResetColor();
            Console.Clear();
            Console.SetCursorPosition(0, 0);



        }

        static bool thistime = false;

        private static void MatrixStep(int width, int height, int[] y, int[] l)
        {
            int x;
            thistime = !thistime;

            for (x = 0; x < width; ++x)
            {
                if (x % 11 == 10)
                {
                    if (!thistime)
                        continue;

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(x, inBoxY(y[x] - 2 - (l[x] / 40 * 2), height));
                    Console.Write(R);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.SetCursorPosition(x, y[x]);
                Console.Write(R);
                y[x] = inBoxY(y[x] + 1, height);
                Console.SetCursorPosition(x, inBoxY(y[x] - l[x], height));
                Console.Write(" ");
            }
        }

        private static void Initialize(out int width, out int height, out int[] y, out int[] l)
        {
            int h1;
            int h2 = (h1 = (height = Console.WindowHeight) / 2) / 2;
            width = Console.WindowWidth - 1;
            y = new int[width];
            l = new int[width];
            int x;
            Console.Clear();
            for (x = 0; x < width; ++x)
            {
                y[x] = r.Next(height);
                l[x] = r.Next(h2 * ((x % 11 != 10) ? 2 : 1), h1 * ((x % 11 != 10) ? 2 : 1));
            }
        }

        static Random r = new Random();

        static char R
        {
            get
            {
                int t = r.Next(10);
                if (t <= 2)
                    return (char)('0' + r.Next(10));
                else if (t <= 4)
                    return (char)('a' + r.Next(27));
                else if (t <= 6)
                    return (char)('A' + r.Next(27));
                else
                    return (char)(r.Next(32, 255));
            }
        }

        public static int inBoxY(int n, int height)
        {
            n = n % height;
            if (n < 0)
                return n + height;
            else
                return n;
        }
    }
}

