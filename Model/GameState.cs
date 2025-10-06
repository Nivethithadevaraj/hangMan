using System.Collections.Generic;

namespace HangmanGame.Model
{
    public class GameState
    {
        public Word CurrentWord { get; private set; }
        public int AttemptsLeft { get; set; }
        public List<char> GuessedLetters { get; private set; }
        public bool IsGameOver { get; set; }

        public GameState(Word word, int attempts)
        {
            CurrentWord = word;
            AttemptsLeft = attempts;
            GuessedLetters = new List<char>();
            IsGameOver = false;
        }

        // Helper: shows current word with blanks
        public string GetWordProgress()
        {
            var display = "";
            foreach (var ch in CurrentWord.Text)
            {
                display += (GuessedLetters.Contains(ch)) ? $"{ch} " : "_ ";
            }
            return display.Trim();
        }
    }
}
