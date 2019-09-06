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
using System.Windows.Shapes;
using M120Projekt.Data;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for UserShowWindow.xaml
    /// </summary>
    public partial class UserShowWindow : Window
    {
        public User User;
        public UserShowWindow(int userId)
        {
            InitializeComponent();

            User = User.ReadById(userId);
            ShowData();
        }

        private void ShowData()
        {
            txtId.Text = User.Id.ToString();
            txtAnrede.Text = User.Salutation;
            txtEmail.Text = User.Email;
            txtFirstname.Text = User.Firstname;
            txtLastname.Text = User.Lastname;
            txtCreatedAt.Text = User.CreatedAt.ToLocalTime().ToString();
            txtUpdatedAt.Text = User.UpdatedAt.ToLocalTime().ToString();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(User) {Owner = this};
            registrationWindow.ShowDialog();
            ShowData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Element wirklich löschen?", "Löschen", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                User.Delete();
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
