﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            if (e.PropertyName == "Password" || e.PropertyName == "CreatedAt" || e.PropertyName == "UpdatedAt" || e.PropertyName == "Id")
            {
                e.Column = null;
            }

            if (e.PropertyName == "Firstname") e.Column.Header = "Vorname";
            if (e.PropertyName == "Lastname") e.Column.Header = "Nachname";
            if (e.PropertyName == "Salutation") e.Column.Header = "Anrede";
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow() {Owner = this};
            registrationWindow.ShowDialog();
            ListUsers(txtSearch.Text);
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

            ListUsers(txtSearch.Text);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Owner.Show();
        }

        private void DtgAccount_CurrentCellChanged(object sender, EventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnShow.IsEnabled = true;
            _selectedIndex = dtgAccount.SelectedIndex;
        }

        private void DisableButtons()
        {
            btnDelete.IsEnabled = false;
            btnShow.IsEnabled = false;
        }

        private void ListUsers(string query = null)
        {
            DisableButtons();
            Users.Clear();
            Users = string.IsNullOrEmpty(query) ? User.All() : User.LikeEmail(query);
            dtgAccount.ItemsSource = Users;
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userShowWindow = new UserWindow(Users[_selectedIndex].Id) { Owner = this };
            userShowWindow.ShowDialog();
            ListUsers(txtSearch.Text);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListUsers(txtSearch.Text);
        }
    }
}
