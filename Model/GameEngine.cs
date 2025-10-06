using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanGame.Model
{
    public class GameEngine
    {
        private static GameEngine? _instance;
        private readonly IWordRepository _wordRepo;

        private GameEngine(IWordRepository wordRepo)
        {
            _wordRepo = wordRepo;
        }

        // Singleton
        public static GameEngine GetInstance(IWordRepository wordRepo)
        {
            if (_instance == null)
                _instance = new GameEngine(wordRepo);
            return _instance;
        }

        public GameState StartNewGame(string difficulty)
        {
            var words = _wordRepo.GetAllWords()
                .Where(w => w.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (words.Count == 0)
            {
                throw new Exception("No words available for this difficulty!");
            }

            var random = new Random();
            var selectedWord = words[random.Next(words.Count)];

            return new GameState(selectedWord, 3); // start with 3 attempts
        }
    }
}
