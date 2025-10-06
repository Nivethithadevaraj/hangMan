using System;
using HangmanGame.Model;

namespace HangmanGame.View
{
    public class LoginView
    {
        public (string username, string password) PromptCredentials()
        {
            Console.Write("Enter username: ");
            string u = Console.ReadLine() ?? "";

            Console.Write("Enter password: ");
            string p = Console.ReadLine() ?? "";

            return (u.Trim(), p);
        }

        public void ShowLoginSuccess(User user)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Login successful! Role: {user.Role}");
            Console.ResetColor();
        }

        public void ShowLoginFailure()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid credentials. Try again.");
            Console.ResetColor();
        }
    }
}
