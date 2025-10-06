using System.Collections.Generic;

namespace HangmanGame.Model
{
    public interface IWordRepository
    {
        List<Word> GetWordsByDifficulty(string difficulty);
    }
}
