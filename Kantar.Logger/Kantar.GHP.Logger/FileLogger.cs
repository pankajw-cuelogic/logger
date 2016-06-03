using System;
using System.IO;
using log4net;

namespace Kantar.GHP.Logger
{
    public sealed class FileLogger : ILogger
    {
        readonly ILog logger;

        static FileLogger()
        {
            var configDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var configFilePath = Path.Combine(configDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));
        }

        /// <summary>
        /// To write target class name in log file for curresponding log
        /// </summary>
        /// <param name="logClass"></param>
        public FileLogger(Type logClass)
        {
            logger = LogManager.GetLogger(logClass);
        }

        /// <summary>
        /// Logs an entry to all logs.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="LoggingInitializationException">Thrown if logger has not been initialized.</exception>
        public void Log(LoggingLevel loggingLevel, string message)
        {
            Log(loggingLevel, message, null, null);
        }

        /// <summary>
        /// Logs an entry to all logs.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        /// <exception cref="LoggingInitializationException">Thrown if logger has not been initialized.</exception>
        public void Log(LoggingLevel loggingLevel, string message, Exception exception)
        {
            LockLog(loggingLevel, message, null, exception);
        }

        /// <summary>
        /// Logs an entry to all logs.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="message">message.</param>
        /// <param name="userName">Logged in user name.</param>
        /// <param name="role">Logged in user role.</param>
        /// <exception cref="LoggingInitializationException">Thrown if logger has not been initialized.</exception>
        public void Log(LoggingLevel loggingLevel, string message, string userName, string role)
        {
            string messageDetails = "User name : " + (!string.IsNullOrWhiteSpace(userName) ? userName : "") + "\n" + "User Role :"
                + (!string.IsNullOrWhiteSpace(role) ? role : "") + "\n" + message;
            LockLog(loggingLevel, messageDetails, null, null);
        }

        /// <summary>
        /// Logs an entry to all logs.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="message">message.</param>
        /// <param name="loggingProperties">Any additional properties for the log as defined in the logging configuration.</param>
        /// <param name="exception">Any exception to be logged.</param>
        private void LockLog(LoggingLevel loggingLevel, string message, object loggingProperties, Exception exception)
        {
            LogBase(logger, loggingLevel, message, loggingProperties, exception);
        }

        /// <summary>
        /// Reason : Lock log if log level is enabled
        /// </summary>
        /// <param name="log"></param>
        /// <param name="loggingLevel"></param>
        /// <param name="message"></param>
        /// <param name="loggingProperties"></param>
        /// <param name="exception"></param>
        private void LogBase(ILog log, LoggingLevel loggingLevel, string message, object loggingProperties, Exception exception)
        {
            if (IsLogEnabled(log, loggingLevel))
            {
                switch (loggingLevel)
                {
                    case LoggingLevel.Debug: log.Debug(message, exception); break;
                    case LoggingLevel.Info: log.Info(message, exception); break;
                    case LoggingLevel.Warning: log.Warn(message, exception); break;
                    case LoggingLevel.Error: log.Error(message, exception); break;
                    case LoggingLevel.Fatal: log.Fatal(message, exception); break;
                }
            }
        }

        private static bool IsLogEnabled(ILog log, LoggingLevel loggingLevel)
        {
            switch (loggingLevel)
            {
                case LoggingLevel.Debug: return log.IsDebugEnabled;
                case LoggingLevel.Info: return log.IsInfoEnabled;
                case LoggingLevel.Warning: return log.IsWarnEnabled;
                case LoggingLevel.Error: return log.IsErrorEnabled;
                case LoggingLevel.Fatal: return log.IsFatalEnabled;
                default: return false;
            }
        }
    }
}
