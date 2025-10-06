using System.Collections.Generic;

namespace HangmanGame.Model
{
    public interface IWordRepository
    {
        List<string> GetWords(string difficulty);
    }
}
