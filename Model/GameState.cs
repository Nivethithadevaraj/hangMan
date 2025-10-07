using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanGame.Model
{
    public class GameState
    {
        public string CurrentWord { get; private set; }
        public int AttemptsLeft { get; private set; }
        public int MaxAttempts { get; private set; }

        private HashSet<char> CorrectGuesses = new();
        private HashSet<char> WrongGuesses = new();
        private char[] Revealed;

        public GameState(string word, int attempts)
        {
            CurrentWord = word.ToLower();
            MaxAttempts = attempts;
            AttemptsLeft = MaxAttempts;
            Revealed = Enumerable.Repeat('_', word.Length).ToArray();
        }

        public string GetWordProgress()
        {
            return string.Join(" ", Revealed);
        }

        public bool ProcessGuess(char guess)
        {
            guess = char.ToLower(guess);

            // ? Already guessed check
            if (CorrectGuesses.Contains(guess) || WrongGuesses.Contains(guess))
            {
                Console.WriteLine("Already tried, enter different letter.");
                return false; // do not reduce attempts
            }

            if (CurrentWord.Contains(guess))
            {
                CorrectGuesses.Add(guess);

                for (int i = 0; i < CurrentWord.Length; i++)
                {
                    if (CurrentWord[i] == guess)
                        Revealed[i] = guess;
                }

                return true; // correct guess
            }
            else
            {
                WrongGuesses.Add(guess);
                AttemptsLeft--; // reduce only for NEW wrong guess
                return false;
            }
        }

        public IEnumerable<char> GetCorrectGuesses() => CorrectGuesses;
        public IEnumerable<char> GetWrongGuesses() => WrongGuesses;

        public bool IsGameOver() => AttemptsLeft <= 0 || IsWin();
        public bool IsWin() => !Revealed.Contains('_');
    }
}
