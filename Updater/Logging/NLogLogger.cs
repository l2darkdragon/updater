using System;
using System.IO;

using NLog;
using NLog.Config;
using NLog.Targets;

namespace Updater.Logging
{
    public class NLogLogger : ILogger
    {
        private readonly Logger mLogger;

        public NLogLogger()
        {
            mLogger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            mLogger.Info(message);
        }

        public void Warn(string message)
        {
            mLogger.Warn(message);
        }

        public void Debug(string message)
        {
            mLogger.Debug(message);
        }

        public void Error(string message)
        {
            mLogger.Error(message);
        }

        public void Error(Exception x)
        {
            mLogger.Error(this.BuildExceptionMessage(x));
        }

        public void Fatal(string message)
        {
            mLogger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            mLogger.Fatal(this.BuildExceptionMessage(x));
        }

        public static void ConfigureLogger()
        {
            // Step 1. Create configuration object 
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties 
            var logPath = App.GetUserAppDataPath();

            fileTarget.FileName = Path.Combine(logPath, "Updater.log");
            fileTarget.ArchiveFileName = Path.Combine(logPath, "Updater.{#####}.log");
            fileTarget.ArchiveAboveSize = 10240;
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Sequence;
            fileTarget.ConcurrentWrites = true;
            fileTarget.KeepFileOpen = false;

            fileTarget.Layout = "${longdate} | ${level} | ${message}";

            LoggingRule rule2 = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;
        }
    }
}
