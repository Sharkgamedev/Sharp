using System;
using System.Diagnostics;
using System.Reflection;
using Sharp.Common.Files;
using Sharp.Common.Utility;

namespace Sharp.Common.Logging
{
    public static class Logger
    {
        private static readonly string r_logDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private const string c_logFileName = "Log.sid";

        /// <summary>
        /// How bad is an error, based on whether or not it affects the 
        /// desired behaviour from being achieved
        /// </summary>
        public enum ErrorLevel
        {
            /// <summary>
            /// Something important
            /// </summary>
            Info,
            /// <summary>
            /// Something went wrong, but it won't affect anything
            /// </summary>
            Warning,
            /// <summary>
            /// Something went wrong and it has affected something
            /// </summary>
            Error
        }

        /// <summary>
        /// Logs an event to the log file, console and in app information system
        /// </summary>
        /// <param name="Message"> The event message </param>
        /// <param name="LogLevel"> The events affect </param>
        public static void Log(string Message, ErrorLevel LogLevel)
        {
            Message = TimeHelper.ExactTimeString + " - " + LogLevel.ToString()
                + ": " + Message;

            FileHandler.EnsureFileExists(r_logDirectory + @$"\{Assembly.GetCallingAssembly().GetName().Name}\Logs\{c_logFileName}");
            FileHandler.AddToFile(r_logDirectory + @$"\{Assembly.GetCallingAssembly().GetName().Name}\Logs\", c_logFileName, Message);
            Debug.WriteLine(Message);
        }
    }
}
