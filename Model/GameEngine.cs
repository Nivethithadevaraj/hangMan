using System;
using System.Linq;

namespace HangmanGame.Model
{
    public class GameEngine
    {
        private readonly IWordRepository wordRepository;
        private GameState gameState=new GameState();

        public GameEngine(IWordRepository repo)
        {
            wordRepository = repo;
        }

        public GameState StartNewGame(string difficulty)
        {
            var words = wordRepository.GetWordsByDifficulty(difficulty).ToList();
            var random = new Random();
            string chosen = words[random.Next(words.Count)].Text;

            gameState = new GameState
            {
                CurrentWord = chosen
            };

            return gameState;
        }

        public GameState GetState() => gameState;

        public void MakeGuess(char guess)
        {
            if (gameState.GuessedLetters.Contains(guess)) return;

            if (gameState.CurrentWord.Contains(guess))
            {
                gameState.GuessedLetters.Add(guess);
                gameState.AttemptsLeft = 3; // restore
            }
            else
            {
                gameState.AttemptsLeft--;
            }
        }

        public bool IsGameOver() => gameState.AttemptsLeft <= 0 || gameState.IsWordGuessed();

        public bool IsWin() => gameState.IsWordGuessed();
    }
}
