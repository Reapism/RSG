using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSG.View.Views
{
    /// <summary>
    /// Interaction logic for RangeControl.xaml
    /// </summary>
    public partial class RangeControl : UserControl
    {
        private Range ValueRange { get; set; }

        public RangeControl()
        {
            InitializeComponent();
        }

        private void TextBlock_Error(object sender, ValidationErrorEventArgs e)
        {

        }

        private string FormatRange()
        {
            return $"{ValueRange.Start}..{ValueRange.End}";
        }

    }
}
