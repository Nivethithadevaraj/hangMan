using System;
using HangmanGame.Model;

namespace HangmanGame.View
{
    public class ConsoleView
    {
        public string ChooseDifficulty()
        {
            Console.WriteLine("Choose difficulty: easy / medium / hard");
            return Console.ReadLine() ?? "easy";
        }

        public void ShowGameStart(GameState state)
        {
            Console.WriteLine($"Word selected! Difficulty: {state.CurrentWord.Difficulty}");
            Console.WriteLine(state.GetWordProgress());
            Console.WriteLine($"Attempts left: {state.AttemptsLeft}");
        }
    }
}
