using System;
using HangmanGame.Model;
namespace HangmanGame.View
{
    public class LoginView
    {
        public (string Username, string Password) PromptCredentials()
        {
            Console.Write("Enter username: ");
            string u = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter password: ");
            string p = (Console.ReadLine() ?? "").Trim();

            return (u, p);
        }

        public void ShowLoginFailure()
        {
            Console.WriteLine("Invalid credentials. Try again or Register.");
            Console.WriteLine("Press L to Login again, R to Register.");
        }

        public void ShowLoginSuccess(User user)
        {
            Console.WriteLine($"Login successful! Welcome {user.Username} ({user.Role})");
        }

        public (string Username, string Password) PromptRegistration()
        {
            Console.WriteLine("=== Register New User ===");

            string username = "";
            while (string.IsNullOrWhiteSpace(username))
            {
                Console.Write("Enter new username: ");
                username = (Console.ReadLine() ?? "").Trim();
            }

            string password = "";
            while (string.IsNullOrWhiteSpace(password))
            {
                Console.Write("Enter new password: ");
                password = (Console.ReadLine() ?? "").Trim();
            }

            return (username, password);
        }
    }
}
