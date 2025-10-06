using HangmanGame.Controller;
using HangmanGame.Game;
using HangmanGame.View;

class Program
{
    static void Main()
    {
        var engine = new GameEngine();
        var view = new ConsoleView();
        var controller = new GameController(engine, view);

        controller.Run();
    }
}
