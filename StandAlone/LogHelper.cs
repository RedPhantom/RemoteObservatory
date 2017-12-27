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

        public enum MessageLevels
        {
            Info,
            Warning,
            Error
        }

        public LogHelper New(string LogFileLocation)
        {
            this.LogFileLocation = LogFileLocation;
            return this;
        }

        public void WriteLog(string Message, MessageLevels Level = MessageLevels.Info)
        {
            switch (Level)
            {
                case MessageLevels.Info:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(DateTime.Now.ToShortTimeString());
                    Console.ResetColor();
                    Console.Write(" ");
                    Console.WriteLine(Message);

                    break;
                case MessageLevels.Warning:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(DateTime.Now.ToShortTimeString());
                    Console.ResetColor();
                    Console.Write(" ");
                    Console.WriteLine(Message);
                    break;

                case MessageLevels.Error:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(DateTime.Now.ToShortTimeString());
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                    Console.WriteLine(Message);
                    break;
                default:
                    break;
            }
        }
    }
}
