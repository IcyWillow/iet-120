using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using M120Projekt.Helper;
using M120Projekt.Model;

namespace M120Projekt
{
    public class GameEngine
    {
        public SoundPlayer SndPlayer = new SoundPlayer();
        public int Points = 0;
        public int GibbetState = 0;
        public string GameWord;

        private bool _isMusicOn = true;
        private List<char> _guesses = new List<char>();
        private Difficulty _difficulty;

        public GameEngine(Difficulty difficulty)
        {
            _difficulty = difficulty;
            SelectRandomWord();
        }

        public void MusicOn(bool value)
        {
            _isMusicOn = value;
        }

        public void PlayMusic(string File)
        {
            SndPlayer.SoundLocation = Environment.CurrentDirectory + "//Sounds//" + File + ".wav";
            if (_isMusicOn)
            {
                SndPlayer.PlayLooping();

                SndPlayer.Stop();

                SndPlayer.PlayLooping();
            }

        }

        public void Guess(string guess, out string message)
        {
            if (string.IsNullOrEmpty(guess))
            {
                message = GameMessage(Message.Empty);
                return;
            }

            if (!Regex.IsMatch(guess, @"^[a-zA-Z]+$"))
            {
                IncrementGibbetState();
                message = GameMessage(Message.IllegalCharacter);
                return;
            }

            if (_guesses.Contains(guess[0]))
            {
                IncrementGibbetState();
                message = GameMessage(Message.AlreadyGuessed);
                return;
            }

            if (!GameWord.Contains(guess))
            {
                IncrementGibbetState();
                message = GameMessage(Message.Wrong);
                _guesses.Add(guess[0]);
                return;
            }

            _guesses.Add(guess[0]);
            message = GameMessage(Message.Correct);
        }

        public void NullifyNegativePoints()
        {
            if (Points < 0) Points = 0;
        }

        private void IncrementGibbetState()
        {
            GibbetState++;
        }

        public string GetGuesses()
        {
            string guesses = "";
            foreach (char c in _guesses) guesses += $"{c} ";
            return guesses.ToUpper();
        }

        private void SelectRandomWord()
        {
            //TODO Implement select difficulty
            List<Word> words = Word.AllActive();
            Random rnd = new Random();
            GameWord = words[rnd.Next(0, words.Count)].Name.ToLower();
        }

        public BitmapImage GibbetImage()
        {
            switch (GibbetState)
            {
                case 1:
                    return ConvertBitmapToBitmapImage.Convert(Properties.Resources.KaraGibbet1);
                case 2:
                    return ConvertBitmapToBitmapImage.Convert(Properties.Resources.KaraGibbet2);
                case 3:
                    return ConvertBitmapToBitmapImage.Convert(Properties.Resources.KaraGibbet3);
                case 4:
                    return ConvertBitmapToBitmapImage.Convert(Properties.Resources.KaraGibbet4);
                case 5:
                    return ConvertBitmapToBitmapImage.Convert(Properties.Resources.KaraGibbet5);
                    default:
                        return null;
            }
        }

        public string DisguisedWord()
        {
            string disguisedWord = string.Empty;

            for (int i = 0; i < GameWord.Length; i++)
            {
                if (_guesses.Contains(GameWord[i]))
                {
                    disguisedWord += $"{GameWord[i]} ";
                }
                else
                {
                    disguisedWord += "_ ";
                }
            }

            return disguisedWord.ToUpper();
        }

        public bool IsGameWon()
        {
            string gameWord = DisguisedWord();
            return !gameWord.Contains("_");
        }

        public bool IsGameOver()
        {
            return GibbetState >= 5;
        }

        public string GameMessage(Message message)
        {
            switch (message)
            {
                case Message.Wrong:
                    Points -= 50 * Convert.ToInt32(_difficulty);
                    return "Diese Buchstabe ist nicht vorhanden. Sie wurden bestraft!";
                case Message.AlreadyGuessed:
                    Points -= 25 * Convert.ToInt32(_difficulty);
                    return "Sie haben diese Buchstabe bereits eingegeben. Sie wurden bestraft!";
                case Message.IllegalCharacter:
                    Points -= 10 * Convert.ToInt32(_difficulty);
                    return "Geben Sie bitte nur gültige Buchstaben ein. Sie wurden bestraft!";
                case Message.Correct:
                    Points += 50 * Convert.ToInt32(_difficulty);
                    return "Ihre Eingabe war korrekt!";
                case Message.Empty:
                    return "Bitte geben Sie eine Buchstabe ein.";
            }

            return "";
        }
    }

    public enum Message
    {
        Wrong = 0,
        AlreadyGuessed = 1,
        IllegalCharacter = 2,
        Correct = 3,
        Empty = 4
    }
}