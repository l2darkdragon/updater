using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Spinner.xaml
    /// </summary>
    public partial class Spinner : UserControl
    {
        /// <summary>
        /// Dependancy property for IsSpinning property.
        /// </summary>
        public static DependencyProperty IsSpinningProperty = DependencyProperty.Register("IsSpinning", typeof(Boolean), typeof(Spinner), new PropertyMetadata(true, new PropertyChangedCallback(IsSpinningChanged)));

        /// <summary>
        /// Gets or sets value indicating whether the spinner is spinning.
        /// </summary>
        public Boolean IsSpinning
        {
            get
            {
                return (bool)GetValue(IsSpinningProperty);
            }
            set
            {
                SetValue(IsSpinningProperty, value);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Spinner()
        {
            InitializeComponent();
        }

        private static void IsSpinningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
 	        System.Diagnostics.Debug.WriteLine("Property " + e.Property + " has changed its value from " + e.OldValue + " to " + e.NewValue);
        }

    }
}
