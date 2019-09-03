using System;
using System.Windows;
using M120Projekt.Data;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private MachineState State = MachineState.Create;
        private bool IsOwnAccount = false;
        private User User;

        public RegistrationWindow(User user = null, bool isOwnAccount = false)
        {
            InitializeComponent();
            if (user != null)
            {
                IsOwnAccount = isOwnAccount;
                State = MachineState.Update;
                User = user;
                cbxSalutation.Text = user.Salutation;
                txtEmail.Text = user.Email;
                txtFirstname.Text = user.Firstname;
                txtLastname.Text = user.Lastname;
                Title = "World of Cara - User ändern";
;               btnRegister.Content = "ändern";
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
            User user;
            user = (User == null) ? new User() : User;
            user.Salutation = cbxSalutation.Text;
            user.Firstname = txtFirstname.Text;
            user.Lastname = txtLastname.Text;
            user.Email = txtEmail.Text;
            // hash the password
            user.Password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);


            if (txtPassword.Password.Equals(txtPasswordConfirmation.Password))
            {
                switch (State)
                {
                    case (MachineState.Create):
                        user.Create();
                        MessageBox.Show("User erstellt.");
                        break;
                    case (MachineState.Update):
                        user.Update();
                        if(IsOwnAccount) Session.Start(user);
                        MessageBox.Show("User verändert.");
                        break;
                }
                Close();
                Owner.Show();
            }
        }
    }

}
