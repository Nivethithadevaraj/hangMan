using System;
using HangmanGame.Model;

namespace HangmanGame.View
{
    public class LoginView
    {
        public User? ShowLogin()
        {
            Console.WriteLine("=== Welcome to Hangman ===");
            Console.Write("Enter username: ");
            var username = Console.ReadLine();

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var repo = new UserRepository();
            var user = repo.GetUser(username ?? "", password ?? "");

            if (user != null)
            {
                Console.WriteLine($"Login successful! Role: {user.Role}");
            }
            else
            {
                Console.WriteLine("Invalid credentials!");
            }

            return user;
        }
    }
}
