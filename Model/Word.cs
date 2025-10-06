namespace HangmanGame.Model
{
    public class Word
    {
        public string Text { get; }
        public string Difficulty { get; }

        public Word(string text, string difficulty)
        {
            Text = text;
            Difficulty = difficulty;
        }
    }
}
