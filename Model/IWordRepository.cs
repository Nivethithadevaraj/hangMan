using System.Collections.Generic;

namespace HangmanGame.Model
{
    public interface IWordRepository
    {
        IEnumerable<Word> GetWordsByDifficulty(string difficulty);
    }
}
