using System.Windows;
using M120Projekt.Data;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private int _state;
        public RegistrationWindow(User user)
        {
            InitializeComponent();
            if (user != null)
            {

            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            //TODO implement validation
            User user = new User();
            user.Salutation = cbxSalutation.Text;
            user.Firstname = txtFirstname.Text;
            user.Lastname = txtLastname.Text;
            user.Email = txtEmail.Text;
            if (txtPassword.Password.Equals(txtPasswordConfirmation.Password))
            {
                // hash the password
                user.Password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
                user.Create();
                MessageBox.Show("User erstellt.");
                Close();
                Owner.Show();
            }
        }
    }

    public enum RegisterWindow
    {
        Create = 1,
        Edit = 2
    }
}
