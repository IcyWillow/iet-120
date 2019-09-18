using System;
using System.Collections.Generic;
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
using M120Projekt.Enum;
using M120Projekt.Model;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for HighscoreWindow.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {
        public HighscoreWindow()
        {
            InitializeComponent();
            uscEasy.gridHighscore.ItemsSource = Highscore.AllByDifficulty(Difficulty.Easy);
            uscMedium.gridHighscore.ItemsSource = Highscore.AllByDifficulty(Difficulty.Medium);
            uscHard.gridHighscore.ItemsSource = Highscore.AllByDifficulty(Difficulty.Hard);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
