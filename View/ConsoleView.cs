using System;
using System.Linq;
using HangmanGame.Model;

namespace HangmanGame.View
{
    public class ConsoleView
    {
        public void ShowWelcome()
        {
            Console.WriteLine("=== Welcome to Hangman ===");
        }

        public void ShowStart(GameState state)
        {
            Console.WriteLine("Word selected! Attempts left: " + state.AttemptsLeft);
            DisplayProgressAndHangman(state);
        }

        // Called each turn to display progress + hangman
        public void DisplayProgressAndHangman(GameState state)
        {
            ShowHangman(state.AttemptsLeft, isWin: state.IsWin(), isGameOver: state.IsGameOver());

            // Show word progress with colored letters
            var correct = state.GetCorrectGuesses().ToHashSet();
            Console.Write("Word: ");
            foreach (char c in state.CurrentWord)
            {
                if (correct.Contains(c))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(c + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Attempts left: {state.AttemptsLeft}");
            Console.ResetColor();

            // show wrong guesses
            var wrong = state.GetWrongGuesses().ToList();
            if (wrong.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Wrong guesses: ");
                Console.WriteLine(string.Join(", ", wrong));
                Console.ResetColor();
            }
        }

        public char GetGuess()
        {
            while (true)
            {
                Console.Write("Enter a letter: ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please type a letter (not empty).");
                    continue;
                }

                char ch = input.Trim()[0];
                if (!char.IsLetter(ch))
                {
                    Console.WriteLine("Please type a valid letter (a–z).");
                    continue;
                }

                return char.ToLower(ch);
            }
        }

        public void ShowCorrect()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correct!");
            Console.ResetColor();
        }

        public void ShowWrong()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong!");
            Console.ResetColor();
        }

        public void ShowWin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
  +---+
      |
  \O/ |
   |  |
  / \ |
=========
YAYYY YOU WON! ??
");
            Console.ResetColor();
        }

        public void ShowLose(string word)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========
DIED... YOU LOSE ??
");
            Console.ResetColor();
            Console.WriteLine($"The word was: {word}");
        }

        private void ShowHangman(int attemptsLeft, bool isWin, bool isGameOver)
        {
            if (isWin)
            {
                // Celebratory stickman
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"
  +---+
      |
  \O/ |
   |  |
  / \ |
=========");
                Console.ResetColor();
                return;
            }

            if (isGameOver && attemptsLeft <= 0)
            {
                // Final dead hangman
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========");
                Console.ResetColor();
                return;
            }

            // Progressive hangman
            switch (attemptsLeft)
            {
                case 3:
                    Console.WriteLine(@"
  +---+
      | 
      |
      |
      |
      |
=========");
                    break;

                case 2:
                    Console.WriteLine(@"
  +---+
  |   |
      |
      |
      |
      |
=========");
                    break;

                case 1:
                    Console.WriteLine(@"
  +---+
  |   |
  O   |
      |
      |
      |
=========");
                    break;

                case 0:
                    Console.WriteLine(@"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========");
                    break;
            }
        }
    }
}
