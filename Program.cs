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

                // ✅ Phase 2: test loading words
                var wordRepo = new CsvWordRepository();
                var words = wordRepo.GetAllWords();

                Console.WriteLine("\nWords loaded from CSV:");
                foreach (var word in words)
                {
                    Console.WriteLine($"{word.Text} ({word.Difficulty})");
                }
            }
        }
    }
}
