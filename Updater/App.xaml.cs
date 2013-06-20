using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

using Updater.Logging;
using Updater.Models;

namespace Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Application logger.
        /// </summary>
        private static ILogger sLogger;

        /// <summary>
        /// Application configuration.
        /// </summary>
        private static Configuration sConfiguration;

        /// <summary>
        /// Gets update version.
        /// </summary>
        public static VersionNumber Version
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return new VersionNumber { Major = version.Major, Minor = version.Minor };
            }
        }

        /// <summary>
        /// Gets application configuration.
        /// </summary>
        public static Configuration Configuration
        {
            get
            {
                if (sConfiguration == null)
                {
                    sConfiguration = Configuration.Restore();
                }

                return sConfiguration;
            }
        }

        /// <summary>
        /// Gets application logger.
        /// </summary>
        public static ILogger Logger
        {
            get
            {
                if (sLogger == null)
                {
                    NLogLogger.ConfigureLogger();
                    sLogger = new NLogLogger();
                }

                return sLogger;
            }
        }

        /// <summary>
        /// On application startup, setup logger and register it to unhandled exceptions event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Create application data directory
            Directory.CreateDirectory(GetUserAppDataPath());

            // Log every unhandled exception
            Application.Current.DispatcherUnhandledException += (sender, args) =>
            {
                Logger.Error(args.Exception);
            };

            // Open configuration for the first time and update last run property
            Configuration.LastRun = DateTime.Now;
            Configuration.Store();

            base.OnStartup(e);
        }

        /// <summary>
        /// Returns path to application data.
        /// </summary>
        /// <returns></returns>
        public static string GetUserAppDataPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DarkDragon", "Updater");
        }
    }
}
