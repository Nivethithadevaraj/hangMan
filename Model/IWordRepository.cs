using System.Collections.Generic;

namespace HangmanGame.Model
{
    public interface IWordRepository
    {
        // Used by GameEngine (returns the plain words)
        List<string> GetWordsByDifficulty(string difficulty);

        // Admin CRUD operations
        List<Word> GetAllWords();
        void AddWord(Word word);
        void UpdateWord(Word oldWord, Word newWord);
        void DeleteWord(Word word);
    }
}
