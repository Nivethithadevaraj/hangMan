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

        public List<string> GetWordsByDifficulty(string difficulty)
        {
            var result = new List<string>();
            if (!File.Exists(_filePath)) return result;

            var lines = File.ReadAllLines(_filePath);
            int start = 0;
            if (lines.Length > 0 && lines[0].ToLower().Contains("difficulty")) start = 1;

            for (int i = start; i < lines.Length; i++)
            {
                var raw = lines[i];
                if (string.IsNullOrWhiteSpace(raw)) continue;

                var parts = raw.Split(',');
                if (parts.Length < 2) continue;

                var diff = parts[0].Trim();
                var word = parts[1].Trim();

                if (string.Equals(diff, difficulty, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(word);
                }
            }

            return result;
        }
    }
}
