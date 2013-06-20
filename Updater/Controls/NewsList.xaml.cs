using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Updater.Models.Feed;

namespace Updater.Controls
{
    /// <summary>
    /// Interaction logic for NewsList.xaml
    /// </summary>
    public partial class NewsList : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NewsList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Navigate to the URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            if (link != null)
            {
                Process.Start(link.NavigateUri.ToString());
            }
        }
    }
}
