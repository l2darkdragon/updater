using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Practices.Prism.Commands;

using Updater.Controls;
using Updater.Models;
using Updater.Services;

namespace Updater.ViewModels
{
    public class WindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Progress bar label indicating current operation.
        /// </summary>
        private String mLabel = "Ready";

        /// <summary>
        /// Progress bar value. Contain values from 0 to 1000.
        /// </summary>
        private Int32 mProgress = 0;

        /// <summary>
        /// Indicates whether the progress bar value is indeterminate.
        /// </summary>
        private Boolean mIsIndeterminate = false;

        /// <summary>
        /// Indicates an error which occured during updater's primary operation. An error state is
        /// a final state and updater stops operating in this state.
        /// </summary>
        private bool mIsErrorState = false;

        /// <summary>
        /// Indicates background operation.
        /// </summary>
        private bool mIsBusy = false;

        /// <summary>
        /// Indicates whether the Play button is enabled.
        /// </summary>
        private Boolean mIsPlayEnabled = false;

        /// <summary>
        /// Application minimize command.
        /// </summary>
        private DelegateCommand mMinimizeCommand;

        /// <summary>
        /// Application shutdown command.
        /// </summary>
        private DelegateCommand mShutdownCommand;

        /// <summary>
        /// Application settings command.
        /// </summary>
        private DelegateCommand mSettingsCommand;

        /// <summary>
        /// Navigate command.
        /// </summary>
        private DelegateCommand<String> mNavigateCommand;

        /// <summary>
        /// Play button relay command.
        /// </summary>
        private DelegateCommand mPlayCommand;


        #region ViewModels

        /// <summary>
        /// Settings dialog view model.
        /// </summary>
        private SettingsViewModel mSettingsViewModel;

        /// <summary>
        /// Gets settings dialog view model.
        /// </summary>
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return mSettingsViewModel;   
            }
            private set
            {
                if (mSettingsViewModel != value)
                {
                    mSettingsViewModel = value;
                    RaiseChange("SettingsViewModel");
                }
            }
        }


        #endregion ViewModels


        #region Commands

        /// <summary>
        /// Gets the application minimize command.
        /// </summary>
        public DelegateCommand MinimizeCommand
        {
            get
            {
                if (mMinimizeCommand == null)
                {
                    mMinimizeCommand = new DelegateCommand(() => Application.Current.MainWindow.WindowState = WindowState.Minimized);
                }

                return mMinimizeCommand;
            }
        }

        /// <summary>
        /// Gets the application shutdown command.
        /// </summary>
        public DelegateCommand ShutdownCommand
        {
            get
            {
                if (mShutdownCommand == null)
                {
                    mShutdownCommand = new DelegateCommand(() => Application.Current.Shutdown());
                }

                return mShutdownCommand;
            }
        }

        /// <summary>
        /// Gets the application settings command. It inverts property indicating whether settings
        /// dialog is visible or not.
        /// </summary>
        public DelegateCommand SettingsCommand
        {
            get
            {
                if (mSettingsCommand == null)
                {
                    mSettingsCommand = new DelegateCommand(() => SettingsViewModel.IsVisible = !SettingsViewModel.IsVisible);
                }

                return mSettingsCommand;
            }
        }

        /// <summary>
        /// Navigate command.
        /// </summary>
        public DelegateCommand<String> NavigateCommand
        {
            get
            {
                if (mNavigateCommand == null)
                {
                    mNavigateCommand = new DelegateCommand<String>((url) => Process.Start(url));
                }

                return mNavigateCommand;
            }
        }

        /// <summary>
        /// Play button command.
        /// </summary>
        public DelegateCommand PlayCommand
        {
            get
            {
                if (mPlayCommand == null)
                {
                    mPlayCommand = new DelegateCommand(RunGame, CanPlay);
                }

                return mPlayCommand;
            }
        }


        #endregion Commands


        #region Properties

        /// <summary>
        /// Gets or sets progress bar label indicating current operation.
        /// </summary>
        public String Label
        {
            get
            {
                return mLabel;
            }
            set
            {
                if (mLabel != value)
                {
                    mLabel = value;
                    RaiseChange("Label");
                }
            }
        }

        /// <summary>
        /// Gets or sets progress bar value.
        /// </summary>
        public Int32 Progress
        {
            get
            {
                return mProgress;
            }
            set
            {
                int newValue = Math.Max(0, Math.Min(1000, value));
                if (mProgress != newValue)
                {
                    mProgress = newValue;
                    RaiseChange("Progress");
                }
            }
        }

        /// <summary>
        /// Gets or sets property indicating whether to progress bar value is indeterminate.
        /// </summary>
        public Boolean IsIndeterminate
        {
            get
            {
                return mIsIndeterminate;
            }
            set
            {
                if (mIsIndeterminate != value)
                {
                    mIsIndeterminate = value;
                    RaiseChange("IsIndeterminate");
                }
            }
        }

        /// <summary>
        /// Gets value indicating an error which occured during updater's primary operation. An
        /// error state is a final state and updater stops operating in this state.
        /// </summary>
        public Boolean IsErrorState
        {
            get
            {
                return mIsErrorState;
            }
            private set
            {
                if (mIsErrorState != value)
                {
                    mIsErrorState = value;
                    RaiseChange("IsErrorState");
                }
            }
        }

        /// <summary>
        /// Gets orvalue indicating background operation.
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return mIsBusy;
            }
            private set
            {
                if (mIsBusy != value)
                {
                    mIsBusy = value;
                    RaiseChange("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets property indicating whether the Play button is enabled.
        /// </summary>
        public Boolean IsPlayEnabled
        {
            get
            {
                return mIsPlayEnabled;
            }
            set
            {
                if (mIsPlayEnabled != value)
                {
                    mIsPlayEnabled = value;

                    RaiseChange("IsPlayEnabled");
                    mPlayCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Gets update version.
        /// </summary>
        public String Version
        {
            get
            {
                return App.Version.ToString();
            }
        }


        #endregion Properties


        /// <summary>
        /// Constructor
        /// </summary>
        public WindowViewModel()
        {
            // Create settings view model
            SettingsViewModel = new SettingsViewModel(this);
            
            // Fetch updater information
            IsBusy = true;
            Label = "Checking for software updates.";
            Task.Factory.StartNew<UpdaterInfo>(FetchUpdaterInfo)
                .ContinueWith(result => EndFetchUpdaterInfo(result), TaskScheduler.FromCurrentSynchronizationContext());
        }


        /// <summary>
        /// Returns true if the Play is allowed.
        /// </summary>
        /// <returns></returns>
        public Boolean CanPlay()
        {
            return IsPlayEnabled;
        }


        /// <summary>
        /// Runs the game!
        /// </summary>
        private void RunGame()
        {
            System.Diagnostics.Debug.WriteLine("Running the game");
        }



        /// <summary>
        /// Fetches updater information from the remote update service server. This method should run asynchronously.
        /// </summary>
        /// <returns></returns>
        private UpdaterInfo FetchUpdaterInfo()
        {
            RemoteServiceTasks service = new RemoteServiceTasks();
            return service.FetchUpdaterInfo();
        }


        /// <summary>
        /// Processes the asynchronous task result.
        /// </summary>
        /// <param name="task"></param>
        private void EndFetchUpdaterInfo(Task<UpdaterInfo> task)
        {
            if (task.IsFaulted)
            {
                Error("An error has been encountered while contacting update service.");

                // Log the exception that caused the task to end prematurely.
                App.Logger.Error(task.Exception);

                return;
            }

            if (task.Result.Version.CompareTo(App.Version) > 0)
            {
                // Schedule download of the new update
                // task.ContinueWith(() => DownloadUpdater(result.Updater.Link));

                Label = "Download newer version of the updater.";
            }
            else
            {
                Label = "Updater is up-to-date.";
                IsPlayEnabled = true;
            }

            IsBusy = false;
        }


        /// <summary>
        /// Changes the properties bound to UI to represent an error state. Must be run on 
        /// dispatcher thread.
        /// </summary>
        /// <param name="label"></param>
        private void Error(String label)
        {
            Label = label;
            Progress = 1000;
            IsErrorState = true;
            IsBusy = false;
            IsIndeterminate = false;
        }

    }
}
