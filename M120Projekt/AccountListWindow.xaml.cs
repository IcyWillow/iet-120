using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using M120Projekt.Data;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for AccountListWindow.xaml
    /// </summary>
    public partial class AccountListWindow : Window
    {
        public List<User> Users { get; set; }
        private int _selectedIndex = 0;

        public AccountListWindow()
        {
            InitializeComponent();
        }

        private void RefreshList()
        {
            Users = User.All();
            dtgAccount.ItemsSource = Users;
        }

        private void DtgAccount_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Password")
            {
                e.Column = null;
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow() {Owner = this};
            registrationWindow.ShowDialog();
            RefreshList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(Users[_selectedIndex]) { Owner = this };
            registrationWindow.ShowDialog();
            RefreshList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Element wirklich löschen?", "Löschen", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Users[_selectedIndex].Delete();
            }

            RefreshList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }

        private void DtgAccount_CurrentCellChanged(object sender, EventArgs e)
        {
           _selectedIndex = dtgAccount.SelectedIndex;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            RefreshList();
        }
    }
}
