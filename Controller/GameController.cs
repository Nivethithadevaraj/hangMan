using System;
using HangmanGame.Model;
using HangmanGame.View;

namespace HangmanGame.Controller
{
    public class GameController
    {
        private readonly ConsoleView _view;
        private readonly GameEngine _engine;

        public GameController(ConsoleView view, IWordRepository wordRepo)
        {
            _view = view;
            _engine = GameEngine.GetInstance(wordRepo);
        }

        public void StartGame()
        {
            string difficulty = _view.ChooseDifficulty();
            GameState state = _engine.StartNewGame(difficulty);

            _view.ShowGameStart(state);
        }
    }
}
