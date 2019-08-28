using System;
using System.Collections.Generic;
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

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for PingPongControl.xaml
    /// </summary>
    public partial class UCDemo : UserControl
    {
        public UCDemo()
        {
            InitializeComponent();
        }

        private void BtnRed_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Roter Knopf");
        }

        private void BtnYellow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gelber Knopf");
        }

        private void BtnPurple_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Violetter Knopf");
        }

        private void BtnGreen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Grüner Knopf");
        }

    }
}
