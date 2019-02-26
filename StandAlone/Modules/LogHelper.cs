using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone
{
    class LogHelper
    {
        public string LogFileLocation { get; set; }

        //public enum MessageLevels
        //{
        //    Info,
        //    Success,
        //    Warning,
        //    Error
        //}

        public LogHelper New(string LogFileLocation, Scope scope)
        {
            this.LogFileLocation = LogFileLocation;

            LogToFile($"Starting logger for scope {scope.ScopeModel} connected on {scope.ComPort}.");

            if (scope.Dome != null)
                LogToFile($"Logging for the dome connected on {scope.Dome.ComPort} as well.");

            return this;
        }

        public enum MessageTypes
        {
            VERBOSE,
            INFO,
            SUCCESS,
            WARNING,
            ERROR
        }

        public void Write(string s, string tag, MessageTypes type = MessageTypes.INFO, bool newLine = true, bool dropTimestamp = false)
        {
            if (dropTimestamp)
                Console.Write(s);
            else
            {
                Console.Write($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ");
                switch (type)
                {
                    case MessageTypes.VERBOSE:
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                        break;
                    case MessageTypes.INFO:
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        break;
                    case MessageTypes.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        break;
                    case MessageTypes.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;

                        break;
                    case MessageTypes.SUCCESS:
                        Console.ForegroundColor = ConsoleColor.Green;

                        break;
                    default:
                        break;
                }

                Console.Write(type.ToString());

                if (!dropTimestamp)
                    Console.ResetColor();

                Console.Write($" {tag.ToUpper()}]\t{s}");

                if (dropTimestamp)
                    Console.ResetColor();
            }

            if (newLine)
                Console.Write(Environment.NewLine);
        }

        public static void WriteS(string s, string tag, MessageTypes type = MessageTypes.INFO, bool newLine = true, bool dropTimestamp = false)
        {
            if (dropTimestamp)
                Console.Write(s);
            else
            {
                Console.Write($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ");
                switch (type)
                {
                    case MessageTypes.VERBOSE:
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                        break;
                    case MessageTypes.INFO:
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        break;
                    case MessageTypes.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        break;
                    case MessageTypes.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;

                        break;
                    case MessageTypes.SUCCESS:
                        Console.ForegroundColor = ConsoleColor.Green;

                        break;
                    default:
                        break;
                }

                Console.Write(type.ToString() + "\t");

                if (!dropTimestamp)
                    Console.ResetColor();

                Console.Write($"{tag.ToUpper()}\t]  ");

                if (type == MessageTypes.ERROR)
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write($"{s}");

                Console.ResetColor();
                //if (dropTimestamp)
                //    Console.ResetColor();
            }

            if (newLine)
                Console.Write(Environment.NewLine);
        }

        public void LogVerbose(string s)
        {
            LogToFile(s);
            Console.WriteLine(DateTimeOffset.Now.ToString("dd/MM/yyyy HH:mm:ss.ffffff") + " " + s);
        }

        private void LogToFile(string s)
        {
            s = DateTimeOffset.Now.ToString("dd/MM/yyyy HH:mm:ss.ffffff") + "  " + s + "\n";
        }

    }
}
