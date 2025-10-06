using HangmanGame.Controller;
using HangmanGame.Model;

namespace HangmanGame.View
{
    public class LoginView
    {
        private readonly UserController userController;

        public LoginView()
        {
            userController = new UserController();
        }

        public User? ShowLogin()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? "";

            return userController.Login(username, password);
        }
    }
}
