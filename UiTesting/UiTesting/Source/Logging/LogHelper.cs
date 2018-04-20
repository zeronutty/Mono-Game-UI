using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source.Logging
{
    public static class LogHelper
    {
        private static TXTLogger TXTLogger = new TXTLogger();
        private static XMLLogger XMLLogger = new XMLLogger();
        private static ConsoleLogger ConsoleLogger = new ConsoleLogger();

        private static Logger Logger = null;

        public static void Log(LogTarget target, LogLevel logLevel, string message)
        {
            switch (target)
            {
                case LogTarget.TXTFile:
                    Logger = TXTLogger;
                    Logger.LogEvent(logLevel, message);
                    break;
                case LogTarget.XMLFile:
                    Logger = XMLLogger;
                    Logger.LogEvent(logLevel, message);
                    break;
                case LogTarget.Console:
                    Logger = ConsoleLogger;
                    Logger.LogEvent(logLevel, message);
                    break;
            }

            Logger = null;
        }
    }
}
