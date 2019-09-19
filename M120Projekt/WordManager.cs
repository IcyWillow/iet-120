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
        public List<Word> Words { get; private set; }
        private readonly User _user;

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
                Words = string.IsNullOrEmpty(query) ? Word.ReadByCreatorId(_user.Id) : Word.LikeByCreator(query, _user.Id);
            }
            dtg.ItemsSource = Words;
        }
        
    }
}
