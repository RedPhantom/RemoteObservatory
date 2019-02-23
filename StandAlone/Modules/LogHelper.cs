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

        public LogHelper New(string LogFileLocation)
        {
            this.LogFileLocation = LogFileLocation;
            return this;
        }

        public enum messageTypes
        {
            INFO,
            WARNING,
            ERROR
        }

        public void Write(string s, string tag, messageTypes type = messageTypes.INFO, bool newLine = true, bool dropTimestamp = false)
        {
            if (dropTimestamp)
                Console.Write(s);
            else
            {
                Console.Write($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ");
                switch (type)
                {
                    case messageTypes.INFO:
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        break;
                    case messageTypes.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        break;
                    case messageTypes.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;

                        break;
                    default:
                        break;
                }

                Console.Write(type.ToString());

                if (!dropTimestamp)
                    Console.ResetColor();

                Console.Write($" {tag.ToUpper()}] {s}");

                if (dropTimestamp)
                    Console.ResetColor();
            }

            if (newLine)
                Console.Write(Environment.NewLine);
        }

        public static void WriteS(string s, string tag, messageTypes type = messageTypes.INFO, bool newLine = true, bool dropTimestamp = false)
        {
            if (dropTimestamp)
                Console.Write(s);
            else
            {
                Console.Write($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} ");
                switch (type)
                {
                    case messageTypes.INFO:
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        break;
                    case messageTypes.WARNING:
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        break;
                    case messageTypes.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;

                        break;
                    default:
                        break;
                }

                Console.Write(type.ToString());

                if (!dropTimestamp)
                    Console.ResetColor();

                Console.Write($" {tag.ToUpper()}] {s}");

                if (dropTimestamp)
                    Console.ResetColor();
            }

            if (newLine)
                Console.Write(Environment.NewLine);
        }

    }
}
