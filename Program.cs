using HangmanGame.Controller;
using HangmanGame.Model;
using HangmanGame.View;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Welcome to Hangman ===");

        var loginView = new LoginView();
        var user = loginView.ShowLogin();

        if (user == null)
        {
            Console.WriteLine("Login failed!");
            return;
        }

        Console.WriteLine($"Login successful! Role: {user.Role}");

        Console.Write("Choose difficulty: easy / medium / hard: ");
        string? difficulty = Console.ReadLine();

        var repo = new CsvWordRepository("Data/Words.csv");
        var engine = new GameEngine(repo);
        var view = new ConsoleView();
        var controller = new GameController(engine, view);

        controller.StartGame(difficulty ?? "easy");
    }
}
