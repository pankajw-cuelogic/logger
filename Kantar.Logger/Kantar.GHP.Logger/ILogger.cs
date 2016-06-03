using System;

namespace Kantar.GHP.Logger
{
    public interface ILogger
    {
        void Log(LoggingLevel level,string message);
        void Log(LoggingLevel level,string message, Exception exception);
        void Log(LoggingLevel loggingLevel, string message, string userName, string role);
    }
}
