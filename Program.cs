using HangmanGame.View;
using HangmanGame.Model;

class Program
{
    static void Main(string[] args)
    {
        var loginView = new LoginView();
        User? loggedInUser = loginView.ShowLogin();

        if (loggedInUser != null)
        {
            if (loggedInUser.Role == "admin")
            {
                Console.WriteLine("Admin menu will go here...");
            }
            else
            {
                Console.WriteLine("Game starts here...");
            }
        }
    }
}
