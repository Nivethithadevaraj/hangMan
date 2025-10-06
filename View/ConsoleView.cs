using HangmanGame.Model;

namespace HangmanGame.View
{
    public class ConsoleView
    {
        public void ShowStart(GameState state)
        {
            Console.WriteLine($"Word selected! Attempts left: {state.AttemptsLeft}");
            Console.WriteLine(state.GetWordProgress());
        }

        public char GetGuess()
        {
            Console.Write("Enter a letter: ");
            return Console.ReadLine()?.ToLower()[0] ?? ' ';
        }

        public void Update(GameState state)
        {
            Console.WriteLine(state.GetWordProgress());
            Console.WriteLine($"Attempts left: {state.AttemptsLeft}");
        }

        public void ShowResult(bool win, string word)
        {
            if (win)
                Console.WriteLine("?? You won!");
            else
                Console.WriteLine($"? Game Over! The word was: {word}");
        }
    }
}
