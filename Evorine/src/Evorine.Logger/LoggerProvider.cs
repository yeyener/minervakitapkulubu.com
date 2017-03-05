using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSL = Microsoft.Extensions.Logging;
using EvoL = Evorine.Logger.Abstractions;

namespace Evorine.Logger
{
    public class LoggerProvider : EvoL.ILoggerProvider, MSL.ILoggerProvider
    {
        const string DefaultLoggerName = "ApplicationLogger";
        const string ScriptingLoggerName = "ScriptingLogger";
        const string DatabaseLoggerName = "DatabaseLogger";

        readonly NLog.LogFactory factory;
        readonly IDictionary<string, Logger> loggers;
        readonly IServiceProvider serviceProvider;

        public LoggerProvider(IServiceProvider serviceProvider)
        {
            factory = new NLog.LogFactory();
            loggers = new Dictionary<string, Logger>();
            this.serviceProvider = serviceProvider;
        }

        public MSL.ILogger CreateLogger(string categoryName)
        {
            return (MSL.ILogger)GetDefaultLogger();
        }


        public EvoL.ILogger GetDefaultLogger()
        {
            return loggers[DefaultLoggerName];
        }
        public EvoL.ILogger GetDatabaseLogger()
        {
            if (!loggers.ContainsKey(DatabaseLoggerName))
                return GetDefaultLogger();
            return loggers[DatabaseLoggerName];
        }
        public EvoL.ILogger GetScriptingLogger()
        {
            if (!loggers.ContainsKey(ScriptingLoggerName))
                return GetDefaultLogger();
            return loggers[ScriptingLoggerName];
        }


        private NLog.Config.LoggingConfiguration createConfiguration(string loggerName, EvoL.LogSettings settings)
        {
            var config = new NLog.Config.LoggingConfiguration();

            var fileTarget = new NLog.Targets.FileTarget();
            fileTarget.FileName = System.IO.Path.Combine(settings.LoggerDirectory, string.Format("{0}[${{machinename}}].txt", loggerName));
            fileTarget.Layout = "\n${longdate}|${level:uppercase=true}|${machinename}|${logger}|Member:${event-properties:callermember}|Line:${event-properties:callerline}|${exception}|${message}";
            config.AddTarget("file", fileTarget);
            

            var ruleFile = new NLog.Config.LoggingRule("*", fileTarget);

            if (settings.DebugLogEnabled) ruleFile.EnableLoggingForLevel(NLog.LogLevel.Debug);
            else ruleFile.DisableLoggingForLevel(NLog.LogLevel.Debug);
            if (settings.ErrorLogEnabled) ruleFile.EnableLoggingForLevel(NLog.LogLevel.Error);
            else ruleFile.DisableLoggingForLevel(NLog.LogLevel.Error);
            if (settings.FatalLogEnabled) ruleFile.EnableLoggingForLevel(NLog.LogLevel.Fatal);
            else ruleFile.DisableLoggingForLevel(NLog.LogLevel.Fatal);
            if (settings.InfoLogEnabled) ruleFile.EnableLoggingForLevel(NLog.LogLevel.Info);
            else ruleFile.DisableLoggingForLevel(NLog.LogLevel.Info);
            if (settings.TraceLogEnabled) ruleFile.EnableLoggingForLevel(NLog.LogLevel.Trace);
            else ruleFile.DisableLoggingForLevel(NLog.LogLevel.Trace);
            if (settings.WarningLogEnabled) ruleFile.EnableLoggingForLevel(NLog.LogLevel.Warn);
            else ruleFile.DisableLoggingForLevel(NLog.LogLevel.Warn);

            config.LoggingRules.Add(ruleFile);

            return config;
        }
        private Logger createLogger(string name, EvoL.LogSettings settings)
        {
            factory.Configuration = createConfiguration(name, settings);
            loggers.Add(name, new Logger(factory.GetLogger(name)));
            return loggers[name];
        }
        public Logger CreateDefaultLogger(EvoL.LogSettings settings)
        {
            return createLogger(DefaultLoggerName, settings);
        }
        public Logger CreateDatabaseLogger(EvoL.LogSettings settings)
        {
            return createLogger(DatabaseLoggerName, settings);
        }
        public Logger CreateScriptingLogger(EvoL.LogSettings settings)
        {
            return createLogger(ScriptingLoggerName, settings);
        }


        public IEnumerable<EvoL.ILogger> Loggers
        {
            get
            {
                return loggers.Values;
            }
        }


        public void Dispose()
        {

        }
    }
}
