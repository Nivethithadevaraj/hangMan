using HangmanGame.Model;
using HangmanGame.View;

namespace HangmanGame.Controller
{
    public class GameController
    {
        private readonly GameEngine gameEngine;
        private readonly ConsoleView gameView;

        public GameController(GameEngine engine, ConsoleView view)
        {
            gameEngine = engine;
            gameView = view;
        }

        public void StartGame(string difficulty)
        {
            var state = gameEngine.StartNewGame(difficulty);
            gameView.ShowStart(state);

            while (!gameEngine.IsGameOver())
            {
                char guess = gameView.GetGuess();
                gameEngine.MakeGuess(guess);
                gameView.Update(state);
            }

            gameView.ShowResult(gameEngine.IsWin(), state.CurrentWord);
        }
    }
}
