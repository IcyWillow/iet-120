﻿using System;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using M120Projekt.Data;
using M120Projekt.Helper;
using Control = System.Windows.Controls.Control;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.MessageBox;

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

            foreach (Control c in grdRegister.Children)
            {
                if (c.GetType() == typeof(Label))
                {
                    if (c.Tag != null && c.Tag.Equals("error")) c.Visibility = Visibility.Hidden;
                }
            }

            if (user != null)
            {
                //Change state for update
                State = MachineState.Update;
                IsOwnAccount = isOwnAccount;
                User = user;
                cbxSalutation.Text = user.Salutation;
                txtEmail.Text = user.Email;
                txtEmail.IsEnabled = false;
                txtFirstname.Text = user.Firstname;
                txtLastname.Text = user.Lastname;
                Title = "World of Cara - User ändern";
                btnRegister.Content = "ändern";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateForm()) return;
            User user = User ?? new User();
            user.Salutation = cbxSalutation.Text;
            user.Firstname = txtFirstname.Text;
            user.Lastname = txtLastname.Text;
            user.Email = txtEmail.Text;
            // hash the password
            if(!string.IsNullOrEmpty(txtPassword.Password)) user.Password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
            switch (State)
                {
                    case (MachineState.Create):
                        user.Create();
                        MessageBox.Show("Bitte loggen Sie ein.", "User erstellt");
                        break;
                    case (MachineState.Update):
                        user.Update();
                        if (IsOwnAccount) Session.Start(user);
                        MessageBox.Show("User verändert.");
                        break;
                }
                Close();
                Owner.Show();
        }

        private bool ValidateForm()
        {
            //Call lost focus methods.
            TxtFirstname_LostFocus(txtFirstname, null);
            TxtLastname_LostFocus(txtLastname, null);
            TxtEmail_LostFocus(txtEmail, null);

            bool a = !string.IsNullOrEmpty(txtPassword.Password);
            bool b = !string.IsNullOrEmpty(txtPasswordConfirmation.Password);
            bool c = State != MachineState.Update;
            if (string.IsNullOrEmpty(txtPassword.Password) &&
                string.IsNullOrEmpty(txtPasswordConfirmation.Password) &&
                State != MachineState.Update)
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
            if (string.IsNullOrEmpty(txtFirstname.Text)) error = "Bitte geben Sie einen gültigen Namen ein.";
            ValidationHelper.ShowErrors(lblErrorFirstname, error, sender);
        }

        private void TxtLastname_LostFocus(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (txtLastname.Text.Length < 3) error = "Nachname muss mindestens 3 Zeichen beinhalten.";
            if (string.IsNullOrEmpty(txtFirstname.Text)) error = "Bitte geben Sie einen gültigen Namen ein.";
            ValidationHelper.ShowErrors(lblErrorLastname, error, sender);
        }

        private void TxtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            string error = "";
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txtEmail.Text);
            if (User.DoesEmailExist(txtEmail.Text) && State == MachineState.Create) error = "Diese Email ist bereits vergeben";
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

     
    }
}
