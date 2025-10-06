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

        // returns progress like "a _ p _"
        public string GetWordProgress()
        {
            return string.Join(" ", Revealed);
        }

        // Processes the guess:
        // - returns true when the guess is (or was) correct
        // - on a NEW correct letter: reveal all occurrences and reset AttemptsLeft to MaxAttempts
        // - on any wrong guess (even repeated): AttemptsLeft--
        public bool ProcessGuess(char guess)
        {
            guess = char.ToLower(guess);

            // If already known correct, just return true (don't restore)
            if (CorrectGuesses.Contains(guess))
                return true;

            // If correct and not seen before
            if (CurrentWord.Contains(guess))
            {
                CorrectGuesses.Add(guess);

                for (int i = 0; i < CurrentWord.Length; i++)
                {
                    if (CurrentWord[i] == guess)
                        Revealed[i] = guess;
                }

                // restore full attempts on new correct guess
                AttemptsLeft = MaxAttempts;
                return true;
            }

            // Wrong guess: always decrement (even if same wrong letter repeated)
            WrongGuesses.Add(guess);
            AttemptsLeft--;
            return false;
        }

        public IEnumerable<char> GetCorrectGuesses() => CorrectGuesses;
        public IEnumerable<char> GetWrongGuesses() => WrongGuesses;

        public bool IsGameOver() => AttemptsLeft <= 0 || IsWin();
        public bool IsWin() => !Revealed.Contains('_');
    }
}
