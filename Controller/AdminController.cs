using System;
using System.Linq;
using HangmanGame.Model;
using System.Collections.Generic;

namespace HangmanGame.Controller
{
    public class AdminController
    {
        private readonly IWordRepository _wordRepo;

        public AdminController(IWordRepository wordRepo)
        {
            _wordRepo = wordRepo ?? throw new ArgumentNullException(nameof(wordRepo));
        }

        // Called by Program.cs: ShowMenu()
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Admin Menu ===");
                Console.WriteLine("1. View All Words");
                Console.WriteLine("2. Add Word");
                Console.WriteLine("3. Update Word");
                Console.WriteLine("4. Delete Word");
                Console.WriteLine("5. Exit Admin");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1": ViewWords(); break;
                    case "2": AddWord(); break;
                    case "3": UpdateWord(); break;
                    case "4": DeleteWord(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        private void ViewWords()
        {
            var words = _wordRepo.GetAllWords();
            if (words.Count == 0)
            {
                Console.WriteLine("No words found.");
                return;
            }

            Console.WriteLine("\n--- Words ---");
            for (int i = 0; i < words.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {words[i].Difficulty} - {words[i].Text}");
            }
        }

        private void AddWord()
        {
            Console.Write("Enter difficulty (easy/medium/hard): ");
            var diff = (Console.ReadLine() ?? "").Trim().ToLower();
            if (diff != "easy" && diff != "medium" && diff != "hard")
            {
                Console.WriteLine("Invalid difficulty.");
                return;
            }

            Console.Write("Enter word: ");
            var txt = (Console.ReadLine() ?? "").Trim().ToLower();
            if (string.IsNullOrWhiteSpace(txt))
            {
                Console.WriteLine("Empty word not allowed.");
                return;
            }

            _wordRepo.AddWord(new Word(diff, txt));
            Console.WriteLine("Word added.");
        }

        private void UpdateWord()
        {
            var words = _wordRepo.GetAllWords();
            if (words.Count == 0) { Console.WriteLine("No words."); return; }

            ViewWords();
            Console.Write("Enter number to update: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > words.Count)
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            var oldWord = words[idx - 1];
            Console.Write("Enter new difficulty (easy/medium/hard): ");
            var diff = (Console.ReadLine() ?? "").Trim().ToLower();
            Console.Write("Enter new word: ");
            var txt = (Console.ReadLine() ?? "").Trim().ToLower();

            if (string.IsNullOrWhiteSpace(diff) || string.IsNullOrWhiteSpace(txt))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            _wordRepo.UpdateWord(oldWord, new Word(diff, txt));
            Console.WriteLine("Word updated.");
        }

        private void DeleteWord()
        {
            var words = _wordRepo.GetAllWords();
            if (words.Count == 0) { Console.WriteLine("No words."); return; }

            ViewWords();
            Console.Write("Enter number to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > words.Count)
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            var word = words[idx - 1];
            _wordRepo.DeleteWord(word);
            Console.WriteLine("Word deleted.");
        }
    }
}
