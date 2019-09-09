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
using M120Projekt.Model;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for AccountListWindow.xaml
    /// </summary>
    public partial class WordsWindow : Window
    {
        private WordsAll _wordsAll = new WordsAll();
        private MyWords _myWords = new MyWords();
      
        public WordsWindow()
        {
            InitializeComponent();
            grdContent.Children.Add(_wordsAll);
        }

        private void DtgAccount_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "CreatedAt" || e.PropertyName == "UpdatedAt" || e.PropertyName == "Id")
            {
                e.Column = null;
            }

            if (e.PropertyName == "CreatedAt") e.Column.Header = "Erstell am:";
            if (e.PropertyName == "Lastname") e.Column.Header = "Nachname";
            if (e.PropertyName == "Salutation") e.Column.Header = "Anrede";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }

        private void BtnAllWords_Click(object sender, RoutedEventArgs e)
        {
            btnMyWords.IsEnabled = true;
            ChangeGridState(sender, _wordsAll);
        }

        private void BtnMyWords_Click(object sender, RoutedEventArgs e)
        {
            btnAllWords.IsEnabled = true;
            ChangeGridState(sender, _myWords);
        }

        private void ChangeGridState(object sender, UserControl userControl)
        {
            ((Button)sender).IsEnabled = false;
            grdContent.Children.Clear();
            grdContent.Children.Add(userControl);
        }
    }
}
