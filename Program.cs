using System;
using System.IO;
using HangmanGame.Controller;
using HangmanGame.Model;
using HangmanGame.View;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Hangman ===");

            // 1) Login
            var loginCsv = Path.Combine("Data", "login.csv"); // uses your CSV
            var userRepo = new UserRepository(loginCsv);
            var loginView = new LoginView();
            User? user = null;

            while (user == null)
            {
                var (u, p) = loginView.PromptCredentials();
                user = userRepo.GetUser(u, p);
                if (user == null) loginView.ShowLoginFailure();
                else loginView.ShowLoginSuccess(user);
            }

            // 2) choose difficulty (validate)
            string difficulty = "";
            while (true)
            {
                Console.Write("Choose difficulty: easy / medium / hard: ");
                difficulty = (Console.ReadLine() ?? "").Trim().ToLower();
                if (difficulty == "easy" || difficulty == "medium" || difficulty == "hard") break;
                Console.WriteLine("Invalid difficulty. Please type exactly: easy, medium, or hard.");
            }

            // 3) prepare repos / engine / view / controller
            var wordsCsv = Path.Combine("Data", "words.csv"); // expected lines: difficulty,word
            var wordRepo = new CsvWordRepository(wordsCsv);
            var engine = new GameEngine(wordRepo);
            var consoleView = new ConsoleView();
            var controller = new GameController(engine, consoleView);

            // Try starting game (if difficulty has no words, let user pick again)
            while (true)
            {
                try
                {
                    controller.StartGame(difficulty);
                    break; // game finished normally
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write("Please choose another difficulty (easy/medium/hard): ");
                    difficulty = (Console.ReadLine() ?? "").Trim().ToLower();
                }
            }

            Console.WriteLine("Thanks for playing!");
        }
    }
}
