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

        // Returns the displayable progress, e.g. "a _ p _"
        public string GetWordProgress()
        {
            return string.Join(" ", Revealed);
        }

        // Processes a guess.
        // Returns true if the letter is (or was) correct, false if wrong.
        // Important: on a NEW correct guess, AttemptsLeft is reset to MaxAttempts.
        public bool ProcessGuess(char guess)
        {
            guess = char.ToLower(guess);

            // If letter was already guessed before, return whether it was a correct one.
            if (CorrectGuesses.Contains(guess) || WrongGuesses.Contains(guess))
            {
                return CorrectGuesses.Contains(guess);
            }

            if (CurrentWord.Contains(guess))
            {
                // new correct guess -> add and reveal all occurrences
                CorrectGuesses.Add(guess);
                for (int i = 0; i < CurrentWord.Length; i++)
                {
                    if (CurrentWord[i] == guess)
                        Revealed[i] = guess;
                }

                // *** KEY: reset attempts to full on a new correct guess ***
                AttemptsLeft = MaxAttempts;

                return true;
            }
            else
            {
                // new wrong guess -> record and decrement attempts
                WrongGuesses.Add(guess);
                AttemptsLeft--;
                return false;
            }
        }

        public bool IsGameOver() => AttemptsLeft <= 0 || IsWin();

        public bool IsWin() => !Revealed.Contains('_');

        // Optional helpers
        public IEnumerable<char> GetCorrectGuesses() => CorrectGuesses;
        public IEnumerable<char> GetWrongGuesses() => WrongGuesses;
    }
}
