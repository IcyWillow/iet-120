using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using M120Projekt.Data;
using M120Projekt.Enum;
using M120Projekt.Helper;
using M120Projekt.Model;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        private GameEngine _gameEngine;
        private GameState _gameState = GameState.Running;
        private readonly Difficulty _difficulty;

        public GameControl(Difficulty difficulty)
        {
            InitializeComponent();
            _gameEngine = new GameEngine(difficulty);
            _difficulty = difficulty;
            CheckGameWord(_gameEngine.IsRandomWordValid());
            Refresh("Bitte geben Sie eine Buchstabe ein.");
            ShowDifficulty();
            ChangedSoundIcon();
            SoundHelper.PlayMusic();
            imgCara.Source = null;
        }

        private void CheckGameWord(bool isGameValid)
        {
            if (isGameValid) return;

            MessageBox.Show("Kein gültiges Wort für die Schwierigkeit gefunden. Wählen Sie bitte eine andere Schwierigkeit aus.",
                "Kein Wort gefunden");
            ShowMainMenu();
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
            if (result == MessageBoxResult.Yes) ShowMainMenu();
        }

        private void ChangedSoundIcon()
        {
            imgButtonMute.Source = SoundHelper.IsSoundOn
                ? ConvertBitmapToBitmapImage.Convert(Properties.Resources.Unmuted)
                : ConvertBitmapToBitmapImage.Convert(Properties.Resources.Muted);
        }

        private void BtnMute_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.IsSoundOn = !SoundHelper.IsSoundOn;
            ChangedSoundIcon();
            SoundHelper.Play(_gameState);
        }

        private void Refresh(string msg = "")
        {
            imgCara.Source = _gameEngine.GibbetImage();
            lblMessage.Content = msg;
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
                _gameEngine.Guess(txtCommand.Text.ToLower(), out string msg);
               Refresh(msg);

               if (_gameEngine.IsGameWon()) GameWon();
               if (_gameEngine.IsGameOver()) GameOver();
            }
        }

        private void GameOver()
        {
            _gameState = GameState.Lost;
            MessageBoxResult result = MessageBox.Show($"Das Spielwort war {_gameEngine.GameWord.ToUpper()}. \nNochmals spielen?",
                "Niederlage", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) NewGame(); else ShowMainMenu();
        }

        private void GameWon()
        {
            _gameState = GameState.Won;
            SoundHelper.PlayVictorySound();
            lblMessage.Content = "Sie haben das Spiel gewonnen!";
            if (_gameEngine.Points > 0)
            {
                MessageBoxResult resultSave = MessageBox.Show(
                    $"Herzliche Gratullation! Sie haben das Spiel gewonnen.\n\nWort: {_gameEngine.GameWord.ToUpper()}\nPunkte: {_gameEngine.Points}\n\nMöchten Sie Ihr Punktzahl eintragen?",
                    "Sieg", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultSave == MessageBoxResult.Yes) InsertHighscore();

                MessageBoxResult result = MessageBox.Show($"Möchten Sie nochmals spielen?",
                    "Neues Spiel", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes) NewGame(); else ShowMainMenu();
            }
        }

        private void InsertHighscore()
        {
            Highscore highscore = new Highscore();
            highscore.UserId = Session.User.Id;
            highscore.GameWord = _gameEngine.GameWord;
            highscore.Points = _gameEngine.Points;
            highscore.Create();
        }

        private void NewGame()
        {
            _gameEngine = new GameEngine(_difficulty);
            _gameState = GameState.Running;
            SoundHelper.PlayMusic();
            Refresh();
        }

        private void ShowMainMenu()
        {
            MainMenuWindow mainMenuWindow = (MainMenuWindow)Window.GetWindow(this);
            mainMenuWindow.RecoverState();
        }
    }

   
}
