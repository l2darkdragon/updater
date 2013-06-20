using System;
using System.Collections.Generic;
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

namespace Updater.Controls
{
    /// <summary>
    /// Interaction logic for LinksList.xaml
    /// </summary>
    public partial class LinksList : UserControl
    {
        public LinksList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When clicked on the hyperlink, we will open the website.
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
