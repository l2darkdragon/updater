using System;
using System.Windows;
using System.Windows.Controls;

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
        public static DependencyProperty IsSpinningProperty = DependencyProperty.Register("IsSpinning", typeof(Boolean), typeof(Spinner));

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

    }
}
