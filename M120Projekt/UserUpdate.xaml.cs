using M120Projekt.Data;
using M120Projekt.Helper;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for UserUpdate.xaml
    /// </summary>
    public partial class UserUpdate : UserControl
    {
        private User User;

        public UserUpdate()
        {
            InitializeComponent();
            ValidationHelper.HideErrorLabels(grdRegister.Children);
            btnUpdate.Content = "Registrieren";
        }

        public UserUpdate(User user)
        {
            InitializeComponent();
            ValidationHelper.HideErrorLabels(grdRegister.Children);
            btnClose.Visibility = Visibility.Hidden;
            btnClose.IsEnabled = false;
            User = user;
            cbxSalutation.Text = user.Salutation;
            txtEmail.Text = user.Email;
            txtEmail.IsEnabled = false;
            txtFirstname.Text = user.Firstname;
            txtLastname.Text = user.Lastname;
            btnUpdate.IsEnabled = false;
        }

        private void BindUser()
        {
            if (User == null) User = new User();
            User.Salutation = cbxSalutation.Text;
            User.Firstname = txtFirstname.Text;
            User.Lastname = txtLastname.Text;
            User.Email = txtEmail.Text;
            // hash the password
            if (!string.IsNullOrEmpty(txtPassword.Password)) User.Password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
        }

        private void Create()
        {
            if (!ValidateForm()) return;
            BindUser();
            User.Create();
            MessageBox.Show("Bitte loggen Sie ein.", "User erstellt");
            Window.GetWindow(this).Close();
        }

        private void Save()
        {
            if (!ValidateForm()) return;
            BindUser();
            User.Update();
            MessageBox.Show("Erfolgreich gespeichert.", "User aktualisert");
            btnUpdate.IsEnabled = false;
        }

        public bool ValidateForm()
        {
            //Call lost focus methods.
            TxtFirstname_LostFocus(txtFirstname, null);
            TxtLastname_LostFocus(txtLastname, null);
            TxtEmail_LostFocus(txtEmail, null);

            if (string.IsNullOrEmpty(txtPassword.Password) &&
                string.IsNullOrEmpty(txtPasswordConfirmation.Password) &&
                User == null)
            {
                TxtPassword_LostFocus(txtPassword, null);
                TxtPasswordConfirmation_LostFocus(txtPasswordConfirmation, null);
            }

            return ValidationHelper.ValidateForm(grdRegister);
        }

        private void TxtFirstname_LostFocus(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtFirstname.Text.Length < 3) error = "Vorname muss mindestens 3 Zeichen beinhalten.";
            if (string.IsNullOrEmpty(txtFirstname.Text)) error = "Bitte geben Sie einen gültigen Vornamen ein.";
            ValidationHelper.ShowErrors(lblErrorFirstname, error, sender);
        }

        private void TxtLastname_LostFocus(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtLastname.Text.Length < 3) error = "Nachname muss mindestens 3 Zeichen beinhalten.";
            if (string.IsNullOrEmpty(txtLastname.Text)) error = "Bitte geben Sie einen gültigen Nachnamen ein.";
            ValidationHelper.ShowErrors(lblErrorLastname, error, sender);
        }

        private void TxtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            string error = "";
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txtEmail.Text);
            if (User.DoesEmailExist(txtEmail.Text) && User == null) error = "Diese Email ist bereits vergeben";
            if (!match.Success) error = "Ungültige Email. Bitte versuchen Sie erneut.";
            if (string.IsNullOrEmpty(txtEmail.Text)) error = "Bitte geben Sie eine gültige Email ein.";
            ValidationHelper.ShowErrors(lblErrorEmail, error, sender);
        }

        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtPassword.Password.Length < 6) error = "Passwort muss mindestens 6 Zeichen beinhalten.";
            if (string.IsNullOrEmpty(txtPassword.Password)) error = "Bitte geben Sie ein gültiges Passwort ein.";
            ValidationHelper.ShowErrors(lblErrorPassword, error, sender);
        }

        private void TxtPasswordConfirmation_LostFocus(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (!txtPassword.Password.Equals(txtPasswordConfirmation.Password)) error = "Passwörter stimmen nicht überein.";
            if (string.IsNullOrEmpty(txtPasswordConfirmation.Password)) error = "Bitte bestätigen Sie das Passwort.";
            ValidationHelper.ShowErrors(lblErrorPasswordConfirmation, error, sender);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (User == null) Create(); else
            {
                Save();
            }
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnUpdate.IsEnabled = true;
        }

        private void CbxSalutation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdate.IsEnabled = true;
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            btnUpdate.IsEnabled = true;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
