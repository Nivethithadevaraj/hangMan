using System;
using System.Collections.Generic;

namespace HangmanGame.Model
{
    public class GameEngine
    {
        private readonly IWordRepository _wordRepository;
        private GameState? _state;
        private const int DEFAULT_MAX_ATTEMPTS = 3;
        private readonly Random _random = new();

        public GameEngine(IWordRepository repo)
        {
            _wordRepository = repo;
        }

        public GameState StartNewGame(string difficulty)
        {
            var list = _wordRepository.GetWordsByDifficulty(difficulty ?? "");
            if (list == null || list.Count == 0)
                throw new InvalidOperationException($"No words found for difficulty '{difficulty}'");

            string chosen = list[_random.Next(list.Count)];
            _state = new GameState(chosen, DEFAULT_MAX_ATTEMPTS);
            return _state;
        }

        public bool MakeGuess(char guess)
        {
            if (_state == null) throw new InvalidOperationException("Game not started.");
            return _state.ProcessGuess(guess);
        }

        public bool IsGameOver() => _state == null ? true : _state.IsGameOver();
        public bool IsWin() => _state == null ? false : _state.IsWin();
        public GameState GetState() => _state ?? throw new InvalidOperationException("Game not started.");
    }
}
