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
            Users = new List<User>();
            ListUsers();
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
            ListUsers();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(Users[_selectedIndex]) { Owner = this };
            registrationWindow.ShowDialog();
            ListUsers();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Element wirklich löschen?", "Löschen", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Users[_selectedIndex].Delete();
                Users.Remove(Users[_selectedIndex]);
                DisableButtons();
            }

            ListUsers();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }

        private void DtgAccount_CurrentCellChanged(object sender, EventArgs e)
        {
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
            _selectedIndex = dtgAccount.SelectedIndex;
        }

        private void DisableButtons()
        {
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ListUsers(txtSearch.Text);
        }

        private void ListUsers(string query = null)
        {
            DisableButtons();
            Users.Clear();
            Users = string.IsNullOrEmpty(query) ? User.All() : User.LikeEmail(query);
            dtgAccount.ItemsSource = Users;
        }
    }

    public enum Action
    {
        Show = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
