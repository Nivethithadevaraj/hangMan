using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame.Model
{
    public class CsvWordRepository : IWordRepository
    {
        private readonly string _filePath;

        public CsvWordRepository(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(_filePath))
            {
                // create with header
                File.WriteAllText(_filePath, "difficulty,word\n");
            }
        }

        public List<Word> GetAllWords()
        {
            var result = new List<Word>();
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
                var txt = parts[1].Trim();
                result.Add(new Word(diff, txt));
            }

            return result;
        }

        public List<string> GetWordsByDifficulty(string difficulty)
        {
            if (string.IsNullOrWhiteSpace(difficulty)) return new List<string>();
            var lower = difficulty.Trim().ToLower();
            return GetAllWords()
                .Where(w => w.Difficulty.Equals(lower, StringComparison.OrdinalIgnoreCase))
                .Select(w => w.Text)
                .ToList();
        }

        public void AddWord(Word word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            var words = GetAllWords();
            words.Add(word);
            SaveAll(words);
        }

        public void UpdateWord(Word oldWord, Word newWord)
        {
            var words = GetAllWords();
            int idx = words.FindIndex(w => w.Difficulty == oldWord.Difficulty && w.Text == oldWord.Text);
            if (idx >= 0)
            {
                words[idx] = newWord;
                SaveAll(words);
            }
        }

        public void DeleteWord(Word word)
        {
            var words = GetAllWords();
            words.RemoveAll(w => w.Difficulty == word.Difficulty && w.Text == word.Text);
            SaveAll(words);
        }

        private void SaveAll(IEnumerable<Word> words)
        {
            // sort words: easy -> medium -> hard, then alphabetically inside each
            var ordered = words
                .OrderBy(w => DifficultyOrder(w.Difficulty))
                .ThenBy(w => w.Text)
                .ToList();

            var lines = new List<string> { "difficulty,word" };
            lines.AddRange(ordered.Select(w => $"{w.Difficulty},{w.Text}"));
            File.WriteAllLines(_filePath, lines);
        }

        private int DifficultyOrder(string diff)
        {
            return diff.ToLower() switch
            {
                "easy" => 1,
                "medium" => 2,
                "hard" => 3,
                _ => 4
            };
        }
    }
}
