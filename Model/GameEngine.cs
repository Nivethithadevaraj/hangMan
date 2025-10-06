using System;
using System.Collections.Generic;

namespace HangmanGame.Model
{
    public class GameEngine
    {
        private readonly IWordRepository _repository;
        private GameState _state;

        public GameEngine(IWordRepository repository)
        {
            _repository = repository;
        }

        public GameState StartNewGame(string difficulty)
        {
            var words = _repository.GetWords(difficulty);
            if (words.Count == 0)
            {
                throw new InvalidOperationException($"No words found for difficulty '{difficulty}'");
            }

            var random = new Random();
            string word = words[random.Next(words.Count)];
            _state = new GameState(word, 3);
            return _state;
        }

        public bool IsGameOver()
        {
            return _state != null && _state.IsGameOver();
        }

        public bool IsWin()
        {
            return _state != null && _state.IsWin();
        }

        public void MakeGuess(char guess)
        {
            _state?.ProcessGuess(guess);
        }

        public GameState GetState()
        {
            return _state;
        }
    }
}
