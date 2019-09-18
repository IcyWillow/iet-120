using M120Projekt.Data;
using M120Projekt.Helper;
using M120Projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for NewWordWindow.xaml
    /// </summary>
    public partial class NewWordWindow : Window
    {
        private Word _word;

        public NewWordWindow(int wordId = 0)
        {
            InitializeComponent();
            if (wordId != 0)
            {
                _word = Word.ReadById(wordId);
                btnNew.Content = "Ändern";
                wdwWord.Title = "World of Cara - Wört ändern";
                txtWord.Text = _word.Name;
                chkState.IsChecked = _word.IsActive;
                btnNew.IsEnabled = false;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm()) return;
            if(_word == null)
            {
                BindWord();
                _word.Create();
            } else
            {
                BindWord();
                _word.Update();
            }

            Close();
        }

        private void BindWord()
        {
            if (_word == null) _word = new Word();
            _word.UserId = Session.User.Id;
            _word.Name = txtWord.Text;
            _word.IsActive = (bool)chkState.IsChecked;
        }

        private void TxtWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnNew.IsEnabled = true;
            string error = "";
            if (!Regex.IsMatch(txtWord.Text, @"^[a-zA-Z]+$")) error = "Wörter dürfen nur Buchstaben beinhalten";
            if (txtWord.Text.Length < 3) error = "Wort muss mindestens 3 Zeichen beinhalten.";
            if (string.IsNullOrEmpty(txtWord.Text)) error = "Bitte geben Sie ein gültiges Wort ein.";
            ValidationHelper.ShowErrors(lblErrorWord, error, sender);
        }

        public bool ValidateForm()
        {
            //Call text changed method.
            return ValidationHelper.ValidateForm(grdNewWord);
        }

        private void ChkState_Checked(object sender, RoutedEventArgs e)
        {
            btnNew.IsEnabled = true;
        }
    }
}
