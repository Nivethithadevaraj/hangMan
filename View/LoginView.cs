using System;
using HangmanGame.Controller;
using HangmanGame.Model;

namespace HangmanGame.View
{
    public class LoginView
    {
        private readonly UserController _userController;

        public LoginView()
        {
            _userController = new UserController();
        }

        public User? ShowLogin()
        {
            Console.WriteLine("=== Welcome to Hangman ===");
            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? "";

            var user = _userController.Login(username, password);

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
