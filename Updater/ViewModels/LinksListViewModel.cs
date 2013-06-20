using System;

using Updater.Models;

namespace Updater.ViewModels
{
    public class LinksListViewModel : BaseViewModel
    {

        /// <summary>
        /// Gets application version number.
        /// </summary>
        public VersionNumber Version
        {
            get
            {
                return App.Version;
            }
        }

    }
}
