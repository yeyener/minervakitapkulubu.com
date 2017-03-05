using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSL = Microsoft.Extensions.Logging;
using EvoL = Evorine.Logger.Abstractions;
using System.Runtime.CompilerServices;

namespace Evorine.Logger
{
    public class Logger : EvoL.ILogger, MSL.ILogger
    {
        readonly NLog.Logger logger;
        
        public Logger(NLog.Logger logger)
        {
            this.logger = logger;
        }

        public string Name
        {
            get
            {
                return logger.Name;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NLog.NestedDiagnosticsContext.Push(state.ToString());
        }

        public bool IsEnabled(MSL.LogLevel logLevel)
        {
            return logger.IsEnabled(getLogLevel(logLevel));
        }


        private NLog.LogLevel getLogLevel(MSL.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case MSL.LogLevel.Critical: return NLog.LogLevel.Fatal;
                case MSL.LogLevel.Debug: return NLog.LogLevel.Debug;
                case MSL.LogLevel.Error: return NLog.LogLevel.Error;
                case MSL.LogLevel.Information: return NLog.LogLevel.Info;
                case MSL.LogLevel.None: throw new NotImplementedException("Unsupported LogLevel: LogLevel.None");
                case MSL.LogLevel.Trace: return NLog.LogLevel.Trace;
                case MSL.LogLevel.Warning: return NLog.LogLevel.Warn;
                default: throw new InvalidOperationException("Invalid operation: invalid LogLevel");
            }
        }

        public void Log<TState>(MSL.LogLevel logLevel, MSL.EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
#warning Improve here
            var nLogLogLevel = getLogLevel(logLevel);
            var message = string.Empty;
            message = formatter(state, exception);

            if (!string.IsNullOrEmpty(message))
            {
                var eventInfo = NLog.LogEventInfo.Create(nLogLogLevel, logger.Name, message, exception);
                eventInfo.Properties["EventId"] = eventId;
                logger.Log(eventInfo);
            }
        }


        private void Log(NLog.LogLevel level, string message, Exception exception = null, string callerPath = "", string callerMember = "", int callerLine = 0)
        {
            if (!logger.IsEnabled(level)) return;

            var logEvent = new NLog.LogEventInfo(level, callerPath, message) { Exception = exception };
            logEvent.Properties.Add("callerpath", callerPath);
            logEvent.Properties.Add("callermember", callerMember);
            logEvent.Properties.Add("callerline", callerLine);
            logger.Log(logEvent);
        }

        public void Debug(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0)
        {
            Log(NLog.LogLevel.Debug, message, exception, callerPath, callerMember, callerLine);
        }

        public void Error(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0)
        {
            Log(NLog.LogLevel.Error, message, exception, callerPath, callerMember, callerLine);
        }

        public void Fatal(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0)
        {
            Log(NLog.LogLevel.Fatal, message, exception, callerPath, callerMember, callerLine);
        }

        public void Info(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0)
        {
            Log(NLog.LogLevel.Info, message, exception, callerPath, callerMember, callerLine);
        }

        public void Trace(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0)
        {
            Log(NLog.LogLevel.Trace, message, exception, callerPath, callerMember, callerLine);
        }

        public void Warning(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0)
        {
            Log(NLog.LogLevel.Warn, message, exception, callerPath, callerMember, callerLine);
        }
    }
}
