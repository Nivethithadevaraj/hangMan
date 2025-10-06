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
            var loginCsv = Path.Combine("Data", "login.csv");
            var userRepo = new UserRepository(loginCsv);
            var loginView = new LoginView();
            User? user = null;

            while (user == null)
            {
                var (u, p) = loginView.PromptCredentials();
                user = userRepo.GetUser(u, p);

                if (user == null)
                {
                    Console.WriteLine("Invalid credentials. Try again (L) or Register (R)?");
                    string choice = (Console.ReadLine() ?? "").Trim().ToUpper();

                    if (choice == "R")
                    {
                        var (ru, rp) = loginView.PromptRegistration();

                        userRepo.AddUser(new User(ru, rp, "user"));
                        Console.WriteLine("Registration successful! You can now login.");

                        user = new User(ru, rp, "user"); 
                    }
                    else if (choice == "L")
                    {
                        continue; 
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please choose L or R.");
                    }
                }
                else
                {
                    loginView.ShowLoginSuccess(user);
                    Console.Clear();
                }
            }

            // 2) Role check
            if (user.Role.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                var wordsCsv = Path.Combine("Data", "words.csv");
                var wordRepo = new CsvWordRepository(wordsCsv);
                var adminController = new AdminController(wordRepo);
                adminController.ShowMenu();
                Console.WriteLine("Exiting Admin Panel...");
            }
            else
            {
                // 3) Normal user plays the game
                string difficulty = "";
                while (true)
                {
                    Console.Write("Choose difficulty: easy / medium / hard: ");
                    difficulty = (Console.ReadLine() ?? "").Trim().ToLower();
                    if (difficulty == "easy" || difficulty == "medium" || difficulty == "hard") break;
                    Console.WriteLine("Invalid difficulty. Please type exactly: easy, medium, or hard.");
                }

                var wordsCsv = Path.Combine("Data", "words.csv");
                var wordRepo = new CsvWordRepository(wordsCsv);
                var engine = new GameEngine(wordRepo);
                var consoleView = new ConsoleView();
                var controller = new GameController(engine, consoleView);

                bool playAgain = true;
                while (playAgain)
                {
                    try
                    {
                        controller.StartGame(difficulty);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.Write("Please choose another difficulty (easy/medium/hard): ");
                        difficulty = (Console.ReadLine() ?? "").Trim().ToLower();
                        continue;
                    }

                    Console.WriteLine("Do you want to play again? (Y/N): ");
                    string ans = (Console.ReadLine() ?? "").Trim().ToUpper();
                    if (ans == "Y")
                    {
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        playAgain = false;
                    }
                }

                Console.WriteLine("Thanks for playing!");
            }
        }
    }
}
