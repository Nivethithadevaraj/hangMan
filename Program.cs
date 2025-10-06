using HangmanGame.View;
using HangmanGame.Model;
using HangmanGame.Controller;

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
                var view = new ConsoleView();
                var wordRepo = new CsvWordRepository();
                var gameController = new GameController(view, wordRepo);

                gameController.StartGame();
            }
        }
    }
}
