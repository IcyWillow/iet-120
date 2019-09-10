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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for DifficultyControl.xaml
    /// </summary>
    public partial class DifficultyControl : UserControl
    {
        private Difficulty _difficulty = Difficulty.Easy;

        public DifficultyControl()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        { 
            GameControl gameControl = new GameControl(_difficulty);

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = (MainMenuWindow)Window.GetWindow(this);
            mainMenuWindow.RecoverState();
        }

        private void RdbEasy_Checked(object sender, RoutedEventArgs e)
        {
            _difficulty = Difficulty.Easy;
        }

        private void RdbMedium_Checked(object sender, RoutedEventArgs e)
        {
            _difficulty = Difficulty.Medium;
        }

        private void RdbHard_Checked(object sender, RoutedEventArgs e)
        {
            _difficulty = Difficulty.Hard;
        }
    }

    public enum Difficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
}
