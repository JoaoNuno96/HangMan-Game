using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace HangManGame
{
    class Program
    {
        static string Word;
        static char[] WordDash;
        static int Lifes = 6;
        static string Dir = AppContext.BaseDirectory.Substring(0, 56) + @"Repository\";

        static void Main(string[] args)
        {
            Console.WriteLine("HangMan Game");
            Console.WriteLine("This is HangMan Game, you have 6 lifes, you have to guess the word before you lose all the lifes");
            Console.WriteLine("Good Luck!");
            Console.WriteLine();
            Console.WriteLine("Select the category:");
            Console.WriteLine("Animals (A)");
            Console.WriteLine("Colors (C)");
            Console.WriteLine("Name (N)");
            Console.WriteLine("Objects (O)");

            var run = true;
            char Cho = char.Parse(Console.ReadLine());
            string Choice = ProcessCategory(Cho);

            ProcessWord(Choice);
            WordDash = new char[Word.Length];
            ProcessDashWord();


            while (run)
            {
                Console.WriteLine("DASH WORD");
                PrintDashWord();
                Console.WriteLine();
                Console.Write("Select a letter: ");
                string selected = Console.ReadLine().ToUpper();
                char select = char.Parse(selected);

                if (!GuessWord(select))
                {
                    Console.WriteLine("Sadly that letter is not in the word...Try again");
                    Lifes--;
                }

                if (!FullWord())
                {
                    run = false;
                    Console.WriteLine("Congrats! You win the game!");
                }
                if (Lifes <= 0)
                {
                    run = false;
                    Console.WriteLine($"You are out of lifes! You lost, the word was {Word}");
                }

            }

        }

        static string ProcessCategory(char n)
        {

            if(n == 'A' || n == 'a')
            {
                return "ANIMAL";
            }
            else if(n == 'C' || n == 'c')
            {
                return "COLOR";
            }
            else if(n == 'N' || n == 'n')
            {
                return "NAME";
            }
            else if(n == 'O' || n == 'o')
            {
                return "OBJECT";
            }
            else
            {
                return null;
            }
        }

        static void ProcessWord(string directory)
        {

            //recover file
            string Source = Dir + directory + ".txt";
            List<string> Words = new List<string>();

            using (StreamReader sr = File.OpenText(Source))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Words.Add(line);
                }
            }

            //process word
            Random Ram = new Random();
            var Numb = Ram.Next(0, Words.Count);

            Word = Words[Numb].ToUpper();
        }

        static void ProcessDashWord()
        {
            for(int i = 0; i>=0 && i<Word.Length; i++)
            {
                WordDash[i] = '-';
            }
        }


        static void PrintDashWord()
        {
            Console.Write("[ ");
            for(int i = 0; i>= 0 && i < WordDash.Length; i++)
            {
                Console.Write(WordDash[i] + " ");
            }
            Console.Write("]");
        }

        static bool GuessWord(char letter)
        {
            var status = false;

            for (int i = 0; i>=0 && i<Word.Length; i++)
            {
                if(letter == Word[i])
                {
                    status = true;
                    WordDash[i] = letter;
                }
            }
            return status;
        }

        static bool FullWord()
        {
            var status = false;

            for(int i = 0; i>=0&& i < WordDash.Length; i++)
            {
                if (WordDash[i] == '-')
                {
                    status = true;
                }
            }

            return status;
        }
    }
}
