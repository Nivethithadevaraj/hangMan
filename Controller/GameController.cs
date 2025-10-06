using System;
using HangmanGame.Model;
using HangmanGame.View;

namespace HangmanGame.Controller
{
    public class GameController
    {
        private readonly GameEngine _engine;
        private readonly ConsoleView _view;

        public GameController(GameEngine engine, ConsoleView view)
        {
            _engine = engine;
            _view = view;
        }

        // Start and run the game loop; throws up to caller if StartNewGame fails
        public void StartGame(string difficulty)
        {
            var state = _engine.StartNewGame(difficulty); // will throw if no words
            _view.ShowStart(state);

            while (!state.IsGameOver())
            {
                char guess = _view.GetGuess();
                bool correct = _engine.MakeGuess(guess);
                if (correct) _view.ShowCorrect();
                else _view.ShowWrong();

                state = _engine.GetState();
                _view.DisplayProgressAndHangman(state);
            }

            if (state.IsWin())
                _view.ShowWin();
            else
                _view.ShowLose(state.CurrentWord);
        }
    }
}
