using M120Projekt.Data;
using M120Projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace M120Projekt
{
    class WordManager
    {
        public List<Word> Words { get; set; }
        private User _user;

        public WordManager(User user = null)
        {
            Words = new List<Word>();
            _user = user;
        }

        public void ListWords(DataGrid dtg, string query = null)
        {
            Words.Clear();
            if(_user == null)
            {
                Words = string.IsNullOrEmpty(query) ? Word.AllActive() : Word.Like(query);
            } else
            {
                Words = string.IsNullOrEmpty(query) ? Word.ReadByCreatorId(_user.Id) : Word.Like(query);
            }
            dtg.ItemsSource = Words;
        }

        public void TextChanged(object sender, DataGrid dtg)
        {
            ListWords(dtg, ((TextBox)sender).Text);
        }
    }
}
