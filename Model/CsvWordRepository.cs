using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame.Model
{
    public class CsvWordRepository : IWordRepository
    {
        private readonly string filePath;

        public CsvWordRepository(string path)
        {
            filePath = path;
        }

        public IEnumerable<Word> GetWordsByDifficulty(string difficulty)
        {
            if (!File.Exists(filePath)) return Enumerable.Empty<Word>();

            var lines = File.ReadAllLines(filePath).Skip(1); // skip header
            return lines
                .Select(line => line.Split(','))
                .Where(parts => parts.Length == 2 && parts[1].Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                .Select(parts => new Word(parts[0], parts[1]));
        }
    }
}
