namespace HangmanGame.Model
{
    public class Word
    {
        public string Difficulty { get; }
        public string Text { get; }

        public Word(string difficulty, string text)
        {
            Difficulty = difficulty.ToLower();
            Text = text.ToLower();
        }

        public override string ToString() => $"{Difficulty},{Text}";
    }
}
