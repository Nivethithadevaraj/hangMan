using System;
using HangmanGame.Game;
using HangmanGame.View;

namespace HangmanGame.Controller
{
    public class GameController
    {
        private GameEngine _engine;
        private ConsoleView _view;

        public GameController(GameEngine engine, ConsoleView view)
        {
            _engine = engine;
            _view = view;
        }

        public void Run()
        {
            Console.WriteLine("=== Welcome to Hangman ===");
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Login successful! Role: user");
            Console.Write("Choose difficulty: easy / medium / hard: ");
            string diff = Console.ReadLine();

            _engine.StartNewGame(diff);
            GameState state = _engine.GetState();

            while (!_engine.IsGameOver())
            {
                _view.DisplayMaskedWord(state);
                _view.DisplayAttempts(state);

                Console.Write("Enter a letter: ");
                char guess = Console.ReadLine()[0];

                bool correct = _engine.MakeGuess(guess);

                if (correct)
                    _view.DisplayMessage("Correct!");
                else
                    _view.DisplayMessage("Wrong!");
            }

            if (_engine.IsWin())
                _view.DisplayMessage("You win!");
            else
                _view.DisplayMessage("Game Over!");
        }
    }
}
