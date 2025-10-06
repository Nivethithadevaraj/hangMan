using System.Collections.Generic;
using System.Linq;

namespace HangmanGame.Model
{
    public class GameState
    {
        public string CurrentWord { get; set; } = string.Empty;
        public HashSet<char> GuessedLetters { get; } = new HashSet<char>();
        public int AttemptsLeft { get; set; } = 3;

        public string GetWordProgress()
        {
            return string.Join(" ", CurrentWord.Select(c => GuessedLetters.Contains(c) ? c : '_'));
        }

        public bool IsWordGuessed()
        {
            return CurrentWord.All(c => GuessedLetters.Contains(c));
        }
    }
}
