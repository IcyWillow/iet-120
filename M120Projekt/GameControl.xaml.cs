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
using M120Projekt.Helper;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        private GameEngine _gameEngine;
        private GameState _gameState = GameState.Running;
        private Difficulty _difficulty;

        public GameControl(Difficulty difficulty)
        {
            InitializeComponent();
            _gameEngine = new GameEngine(difficulty);
            _difficulty = difficulty;
            txtHiddenWord.Text = _gameEngine.DisguisedWord();
            ShowDifficulty();
        }

        private void ShowDifficulty()
        {
            switch (_difficulty)
            {
                case Difficulty.Easy:
                    lblDifficulty.Content = "Einfach";
                    break;
                case Difficulty.Medium:
                    lblDifficulty.Content = "Mittel";
                    break;
                case Difficulty.Hard:
                    lblDifficulty.Content = "Schwierig";
                    break;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Zurück zum Hauptmenü?", "Spiel Beeden", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainMenuWindow mainMenuWindow = (MainMenuWindow)Window.GetWindow(this);
                mainMenuWindow.RecoverState();
            }
        }

        private void BtnMute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh()
        {
            imgCara.Source = _gameEngine.GibbetImage();
            lblMessage.Content = "";
            txtHiddenWord.Text = _gameEngine.DisguisedWord();
            _gameEngine.NullifyNegativePoints();
            lblPoints.Content = _gameEngine.Points.ToString();
            txtGuess.Text = _gameEngine.GetGuesses();
            txtCommand.Text = "";
        }

        private void TxtCommand_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && _gameState == GameState.Running)
            {
                string msg = string.Empty;
               _gameEngine.Guess(txtCommand.Text.ToLower(), out msg);
               Refresh();
               lblMessage.Content = msg;

               if (_gameEngine.IsGameWon())
               {
                   _gameState = GameState.Won;
                   lblMessage.Content = "Sie haben das Spiel gewonnen!";
               }

               if (_gameEngine.IsGameOver())
               {
                   _gameState = GameState.Lost;

                   MessageBoxResult result = MessageBox.Show($"Das Spielwort war {_gameEngine.GameWord.ToUpper()}. \nNochmal spielen?", "Niederlage", MessageBoxButton.YesNo, MessageBoxImage.Question);
                   if (result == MessageBoxResult.Yes)
                   {
                       _gameEngine = new GameEngine(_difficulty);
                       _gameState = GameState.Running;
                       Refresh();
                   }
                   else
                   {
                       MainMenuWindow mainMenuWindow = (MainMenuWindow)Window.GetWindow(this);
                       mainMenuWindow.RecoverState();
                    }
                }
            }
        }
    }

    public enum GameState
    {
        Running = 0,
        Lost = 1,
        Won = 2
    }
}
