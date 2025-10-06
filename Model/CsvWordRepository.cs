using System;
using System.Collections.Generic;
using System.IO;

namespace HangmanGame.Model
{
    public class CsvWordRepository : IWordRepository
    {
        private readonly string _filePath;

        public CsvWordRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<string> GetWords(string difficulty)
        {
            var words = new List<string>();

            if (!File.Exists(_filePath))
                return words;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && parts[0].Trim().Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                {
                    words.Add(parts[1].Trim());
                }
            }

            return words;
        }
    }
}
