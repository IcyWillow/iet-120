using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace iet_120.Engine
{
    public class GameEngine
    {
        public static SoundPlayer sndPlayer = new SoundPlayer();
        public static bool IsMusicOn = true;

        public static void MusicOn(bool value)
        {
            IsMusicOn = value;
        }
        

        public static void playMusic(String File)
        {
            sndPlayer.SoundLocation = Environment.CurrentDirectory + "//Sounds//" + File + ".wav";
            if (IsMusicOn)
            {

                sndPlayer.PlayLooping();

                sndPlayer.Stop();

                sndPlayer.PlayLooping();
            }

        }

        public static string GetGuesses(string guesses)
        {
            string guesslayout = "";

            for (int x = 0; x < guesses.Length; x++)
            {
                guesslayout += guesses[x];
                guesslayout += " ";
            }

            return guesslayout.ToUpper();
        }

        public static string SelectRandomWord()
        {
            string line;
            StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + "\\Resources\\Words.txt");
            Random randomWord = new Random();
            line = file.ReadToEnd();
            string[] arrWords = SplitIntoArray(line);

            int value = randomWord.Next(0, arrWords.Length);

            return arrWords[value];
        }

        static String[] SplitIntoArray(string line)
        {
            int arraySize = Read(line, '\n');
            String WordBank = "";
            String[] array = new String[arraySize];

            for (int i = 0, arrIndex = 0; i < line.Length; i++)
            {
                if (line[i] != '\r')
                {
                    if (line[i] != '\n')
                    {
                        WordBank += line[i];
                    }
                    else
                    {
                        array[arrIndex] = WordBank;
                        WordBank = "";
                        arrIndex++;
                    }
                }
            }

            return array;
        }


        public static string DisguisedWord(string word, char[] guess)
        {
            string wordTemp = word.ToLower();
            string disguisedWord = string.Empty;

            for (int i = 0; word.Length > i; i++)
            {
                if (Read(guess, wordTemp[i]) > 0 || wordTemp[i] == ' ')
                {

                    disguisedWord += word[i];
                    disguisedWord += " ";
                }
                else
                    disguisedWord += "_ ";

            }

            return disguisedWord;
        }

        public static int Read(char[] Line, char comparator)
        {
            String sum = "";

            for (int i = 0; i < Line.Length; i++)
            {
                sum += Line[i];
            }

            return Read(sum, comparator);
        }

        public static int Read(string line, char comparator)
        {
            int WordsTotal = 0;
            int searcher = line.Length - 1;

            while (searcher > -1)
            {
                if (line[searcher] == comparator)
                {
                    WordsTotal++;
                }
                searcher--;
            }

            return WordsTotal;
        }

        public static string[] InsertLine(string[] BaseArray, int NumberOfLine, string[] ToBeAddedArray)
        {
            int ArrayTotal = BaseArray.Length + ToBeAddedArray.Length;
            int WriteContinuation = 0;

            String[] NewArray = new String[ArrayTotal];

            // BaseArray até o NumberOfLine, ToBeAddedArray com For, Resumir o BaseArray end

            for (int i = 0; NumberOfLine > i; i++)
            {
                //Console.WriteLine(BaseArray[i]);
                NewArray[i] += BaseArray[i];
                WriteContinuation++;
            }


            for (int i = 0; i < ToBeAddedArray.Length; i++)
            {
                NewArray[WriteContinuation] += ToBeAddedArray[i];
                WriteContinuation++;
            }



            for (; NumberOfLine < BaseArray.Length; NumberOfLine++)
            {
                //Console.WriteLine(BaseArray[NumberOfLine]);
                NewArray[WriteContinuation] += BaseArray[NumberOfLine];
                WriteContinuation++;
            }

            return NewArray;
        }

    }

    public enum Difficulty
    {
        Diff
    }
}
