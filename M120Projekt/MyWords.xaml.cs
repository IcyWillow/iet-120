using M120Projekt.Data;
using System;
using System.Windows;
using System.Windows.Controls;

namespace M120Projekt
{
    public partial class MyWords : UserControl
    {
        private WordManager _wordManager = new WordManager(Session.User);
        int _selectedIndex = 0;

        public MyWords()
        {
            InitializeComponent();
            _wordManager.ListWords(dtgWords);
        }

        private void DtgWords_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "User" || e.PropertyName == "UserId" || e.PropertyName == "Id" || e.PropertyName == "Creator")
            {
                e.Column = null;
            }

            if (e.PropertyName == "Name") e.Column.Header = "Wort";
            if (e.PropertyName == "CreatedAt") e.Column.Header = "Erstellt am";
            if (e.PropertyName == "UpdatedAt") e.Column.Header = "Aktualisert am";
            if (e.PropertyName == "IsActive") e.Column.Header = "Aktiv?";
        }

        private void DtgWords_CurrentCellChanged(object sender, EventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;
            _selectedIndex = dtgWords.SelectedIndex;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _wordManager.ListWords(dtgWords, txtSearch.Text);
        }

        private void BtnNew_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewWordWindow newWordWindow = new NewWordWindow();
            newWordWindow.ShowDialog();
            _wordManager.ListWords(dtgWords, txtSearch.Text);
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Element wirklich löschen?", "Löschen", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _wordManager.Words[_selectedIndex].Delete();
                _wordManager.Words.Remove(_wordManager.Words[_selectedIndex]);
                btnDelete.IsEnabled = false;
                btnUpdate.IsEnabled = false;
            }

            _wordManager.ListWords(dtgWords, txtSearch.Text);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            NewWordWindow newWordWindow = new NewWordWindow(_wordManager.Words[_selectedIndex].Id);
            newWordWindow.ShowDialog();
            _wordManager.ListWords(dtgWords, txtSearch.Text);
        }
    }
}
