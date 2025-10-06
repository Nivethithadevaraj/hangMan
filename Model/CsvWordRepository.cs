using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame.Model
{
    public class CsvWordRepository : IWordRepository
    {
        private readonly string _filePath = Path.Combine("Data", "words.csv");

        public List<Word> GetAllWords()
        {
            var words = new List<Word>();

            if (!File.Exists(_filePath))
            {
                Console.WriteLine("Words file not found!");
                return words;
            }

            var lines = File.ReadAllLines(_filePath).Skip(1); // skip header
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    words.Add(new Word(parts[0], parts[1]));
                }
            }

            return words;
        }
    }
}
