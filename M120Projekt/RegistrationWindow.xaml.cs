using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using M120Projekt.Data;
using M120Projekt.Helper;
using MessageBox = System.Windows.MessageBox;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

            grdRegister.Children.Add(new UserUpdate());
        }
    }
}
