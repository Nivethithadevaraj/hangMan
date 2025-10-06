using System;
using HangmanGame.Game;

namespace HangmanGame.View
{
    public class ConsoleView
    {
        public void DisplayMaskedWord(GameState state)
        {
            Console.WriteLine(state.GetMaskedWord());
        }

        public void DisplayAttempts(GameState state)
        {
            Console.WriteLine($"Attempts left: {state.AttemptsLeft}");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
