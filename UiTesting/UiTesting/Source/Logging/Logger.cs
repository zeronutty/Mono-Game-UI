using System;
using Microsoft.Xna.Framework;

namespace UiTesting.Source.Logging
{
    public enum LogTarget
    {
        TXTFile,
        XMLFile,
        Console
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Fatal
    }

    public abstract class Logger
    {
        internal string p_Message = string.Empty;

        public abstract void LogEvent(LogLevel logLevel, string message);

        internal void AppendLogLevel(LogLevel logLevel, string message, out string Message)
        {
            Message = string.Empty;

            switch (logLevel)
            {
                case LogLevel.Info:
                    Message = "Info: " + message;
                    return;
                case LogLevel.Warning:
                    Message = "Warning: " + message;
                    return;
                case LogLevel.Error:
                    Message = "Error: " + message;
                    return;
                case LogLevel.Fatal:
                    Message = "Fatal: " + message;
                    return;
            }
        }

        internal void AppendLogLevelColor(LogLevel logLevel, string message, out string Message)
        {
            Message = string.Empty;

            switch (logLevel)
            {
                case LogLevel.Info:
                    Message = "Info: " + message;
                    return;
                case LogLevel.Warning:
                    Message = "{{YELLOW}}Warning" + message;
                    return;
                case LogLevel.Error:
                    Message = "{{ORANGE}}Error: " + message;
                    return;
                case LogLevel.Fatal:
                    Message = "{{RED}}Fatal: " + message;
                    return;
            }
        }
    }

    public class TXTLogger : Logger
    {
        public string filePath;

        public override void LogEvent(LogLevel logLevel, string message)
        {
            
        }
    }

    public class XMLLogger : Logger
    {
        public string filePath;

        public override void LogEvent(LogLevel logLevel, string message)
        {

        }
    }

    public class ConsoleLogger : Logger
    {
        public override void LogEvent(LogLevel logLevel, string message)
        {
#if DEBUG

            AppendLogLevel(logLevel, message, out p_Message);

            Console.WriteLine(p_Message);

            if (UiManager.GetActiveUserInterface().ConsoleWindow != null)
            {
                AppendLogLevelColor(logLevel, message, out p_Message);

                UiManager.GetActiveUserInterface().ConsoleWindow.NewLineText(p_Message);
            }
#else
            
            if(UserInterface.GetActiveUserInterface().ConsoleWindow != null)
            {
                Paragraph paragraph = UserInterface.DefaultParagraph(message, Anchor.AutoInLine, Color.Black, null, new Vector2(0, 20), null);

                UserInterface.GetActiveUserInterface().ConsoleWindow.AddChild(paragraph);
            }
#endif
        }
    }


}
