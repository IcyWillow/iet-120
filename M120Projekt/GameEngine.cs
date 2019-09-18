using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using M120Projekt.Enum;
using M120Projekt.Helper;
using M120Projekt.Model;

namespace M120Projekt
{
    public class GameEngine
    {
        public int Points = 0;
        public int GibbetState = 0;
        public string GameWord;
        private DateTime _lastGuess = DateTime.Now;
        private List<char> _guesses = new List<char>();
        private Difficulty _difficulty;

        public GameEngine(Difficulty difficulty)
        {
            _difficulty = difficulty;
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

        public bool IsRandomWordValid()
        {
            Random rnd = new Random();
            List<Word> words = Word.AllActive();
            bool isWordValid = false;
            int findCounter = 0;

            while (!isWordValid && findCounter < 100)
            {
                GameWord = words[rnd.Next(0, words.Count)].Name.ToLower();
                findCounter++;
                switch (_difficulty)
                {
                    case Difficulty.Easy:
                        if(GameWord.Length <= 5) isWordValid = true;
                        continue;
                    case Difficulty.Medium:
                        if (GameWord.Length > 5 && GameWord.Length <= 9) isWordValid = true;
                        continue;
                    case Difficulty.Hard:
                        if (GameWord.Length > 9) isWordValid = true;
                        continue;
                }
            }

            return isWordValid;
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

            foreach (char c in GameWord)
                disguisedWord += _guesses.Contains(c) ? $"{c} " : "_ ";

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
            Random rnd = new Random();
            int randomPoints = rnd.Next(15, 35) + GetTimeMalus();
            switch (message)
            {
                case Message.Wrong:
                    Points -= 30 * Convert.ToInt32(_difficulty) + randomPoints;
                    return "Diese Buchstabe ist nicht vorhanden. Sie wurden bestraft!";
                case Message.AlreadyGuessed:
                    Points -= 20 * Convert.ToInt32(_difficulty) + randomPoints;
                    return "Sie haben diese Buchstabe bereits eingegeben. Sie wurden bestraft!";
                case Message.IllegalCharacter:
                    Points -= 10 * Convert.ToInt32(_difficulty) + randomPoints;
                    return "Geben Sie bitte nur gültige Buchstaben ein. Sie wurden bestraft!";
                case Message.Correct:
                    Points += 50 * Convert.ToInt32(_difficulty) + randomPoints;
                    return "Ihre Eingabe war korrekt!";
                case Message.Empty:
                    return "Bitte geben Sie eine Buchstabe ein.";
            }

            return "";
        }

        private int GetTimeMalus()
        {
            DateTime now = DateTime.Now;
            double malus = (_lastGuess - now).TotalSeconds;
            _lastGuess = DateTime.Now;
            return (int)malus;
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