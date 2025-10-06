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
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0]))
            {
                Console.WriteLine("Invalid input. Please enter a single letter.");
                return ' ';
            }

            return char.ToLower(input[0]);
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
