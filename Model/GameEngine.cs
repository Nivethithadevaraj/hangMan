using System;

namespace HangmanGame.Game
{
    public class GameEngine
    {
        private GameState _state;
        private string[] _words = { "CAT", "DOG", "APPLE", "HOUSE" };

        public GameEngine()
        {
            _state = new GameState("CAT", 3);
        }

        public void StartNewGame(string difficulty)
        {
            string word = _words[new Random().Next(_words.Length)];
            _state = new GameState(word, 3); // always 3 attempts
        }

        public GameState GetState()
        {
            return _state;
        }

        public bool MakeGuess(char guess)
        {
            guess = Char.ToUpper(guess);

            if (_state.Word.Contains(guess))
            {
                _state.CorrectGuesses.Add(guess);
                _state.AttemptsLeft = 3; // restore on correct guess
                return true;
            }
            else
            {
                _state.WrongGuesses.Add(guess);
                _state.AttemptsLeft--; // reduce on wrong guess
                return false;
            }
        }

        public bool IsGameOver()
        {
            return _state.AttemptsLeft <= 0 || _state.IsWordGuessed();
        }

        public bool IsWin()
        {
            return _state.IsWordGuessed();
        }
    }
}
