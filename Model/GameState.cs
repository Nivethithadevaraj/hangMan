namespace HangmanGame.Game
{
    public class GameState
    {
        public string Word { get; private set; }
        public HashSet<char> CorrectGuesses { get; private set; }
        public HashSet<char> WrongGuesses { get; private set; }
        public int AttemptsLeft { get; set; }

        public GameState(string word, int attempts)
        {
            Word = word.ToUpper();
            AttemptsLeft = attempts;
            CorrectGuesses = new HashSet<char>();
            WrongGuesses = new HashSet<char>();
        }

        public string GetMaskedWord()
        {
            return string.Join(" ", Word.Select(c => CorrectGuesses.Contains(c) ? c : '_'));
        }

        public bool IsWordGuessed()
        {
            return Word.All(c => CorrectGuesses.Contains(c));
        }
    }
}
