using System.Windows.Controls;


namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for WordsAll.xaml
    /// </summary>
    public partial class WordsAll : UserControl
    {
        private WordManager _wordManager = new WordManager();

        public WordsAll()
        {
            InitializeComponent();
            _wordManager.ListWords(dtgWords);
        }

        private void DtgWords_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "User" || e.PropertyName == "UserId" || e.PropertyName == "Id" || e.PropertyName == "IsActive")
            {
                e.Column = null;
            }

            if (e.PropertyName == "Name") e.Column.Header = "Wort";
            if (e.PropertyName == "Creator") e.Column.Header = "Verfasser";
            if (e.PropertyName == "CreatedAt") e.Column.Header = "Erstellt am";
            if (e.PropertyName == "UpdatedAt") e.Column.Header = "Aktualisert am";
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
