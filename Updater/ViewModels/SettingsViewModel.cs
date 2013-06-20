using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Prism.Commands;

using Updater.Models;
using Updater.Services;

namespace Updater.ViewModels
{
    /// <summary>
    /// Settings modal dialog view model.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Window view model.
        /// </summary>
        private WindowViewModel mWindowViewModel;

        /// <summary>
        /// Selected game client.
        /// </summary>
        private ClientInfo mSelectedClient = null;

        /// <summary>
        /// Save settings command.
        /// </summary>
        private DelegateCommand mSaveSettingsCommand;

        /// <summary>
        /// Indicates whether the settings are in error state due to an error while fetching clients
        /// from the remote update service server.
        /// </summary>
        private bool mIsErrorState;

        /// <summary>
        /// Indicates whether the settings modal dialog is visible.
        /// </summary>
        private bool mIsVisible;

        /// <summary>
        /// Indicates clients fetching operation. 
        /// </summary>
        private bool mIsFetching;


        #region Properties


        /// <summary>
        /// Gets value indicating whether the settings dialog is in error state due to an error 
        /// while fetching clients from the remote update service server.
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
        /// Gets value indicating clients fetching operation.
        /// </summary>
        public Boolean IsFetching
        {
            get
            {
                return mIsFetching;
            }
            private set
            {
                if (mIsFetching != value)
                {
                    mIsFetching = value;
                    RaiseChange("IsFetching");
                }
            }
        }

        /// <summary>
        /// Gets or sets property indicating whether the Settings modal dialog is visible.
        /// </summary>
        public Boolean IsVisible
        {
            get
            {
                return mIsVisible;
            }
            set
            {
                if (mIsVisible != value)
                {
                    mIsVisible = value;

                    RaiseChange("IsVisible");
                }
            }
        }

        /// <summary>
        /// Gets collection of game clients fetched via check-in task.
        /// </summary>
        public ObservableCollection<ClientInfo> Clients { get; private set; }

        /// <summary>
        /// Gets or sets the selected client.
        /// </summary>
        public ClientInfo SelectedClient
        {
            get
            {
                return mSelectedClient;
            }
            set
            {
                if (mSelectedClient != value)
                {
                    mSelectedClient = value;

                    // Store value into configuration file as well
                    App.Configuration.Client.Identifier = value.Identifier;

                    RaiseChange("SelectedClient");
                }
            }
        }


        #endregion Properties

        
        /// <summary>
        /// Creates an instance of the Settings modal dialog view model.
        /// </summary>
        /// <param name="windowViewModel"></param>
        public SettingsViewModel(WindowViewModel windowViewModel)
        {
            mWindowViewModel = windowViewModel;

            // Set defaults
            Clients = new ObservableCollection<ClientInfo>();

            // Fetch client list from the remote update service server
            IsFetching = true;

            Task.Factory.StartNew<ClientInfo[]>(FetchClients)
                .ContinueWith(result => EndFetchClients(result), TaskScheduler.FromCurrentSynchronizationContext());
        }


        #region Commands


        /// <summary>
        /// Gets the save settings command. It invokes application configuration store method.
        /// </summary>
        public DelegateCommand SaveSettingsCommand
        {
            get
            {
                if (mSaveSettingsCommand == null)
                {
                    mSaveSettingsCommand = new DelegateCommand(() =>
                    {
                        // Store configuration and hide settings
                        App.Configuration.Store();

                        IsVisible = false;
                    });
                }

                return mSaveSettingsCommand;
            }
        }


        #endregion Commands


        /// <summary>
        /// Fetches client information from the remote update service server. This method should 
        /// run asynchronously.
        /// </summary>
        private ClientInfo[] FetchClients()
        {
            RemoteServiceTasks service = new RemoteServiceTasks();
            return service.FetchClients();
        }

        /// <summary>
        /// Processes the asynchronous task results. Must be run on dispatcher thread.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private void EndFetchClients(Task<ClientInfo[]> task)
        {
            if (task.IsFaulted)
            {
                // In case of fault, set the error state flag
                IsErrorState = true;

                // Log the exception that caused the task to end prematurely.
                App.Logger.Error(task.Exception);
            }
            else
            {
                // Get the check-in result object
                foreach (ClientInfo info in task.Result)
                {
                    Clients.Add(info);

                    if (App.Configuration.Client.Identifier == info.Identifier)
                    {
                        SelectedClient = info;
                    }
                }
            }

            // Fetching operation is completed
            IsFetching = false;
        }

    }
}
