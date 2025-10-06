using System.IO;
using HangmanGame.Model;

namespace HangmanGame.Controller
{
    public class UserController
    {
        private readonly UserRepository _repo;

        public UserController()
        {
            var filePath = Path.Combine("Data", "login.csv");
            _repo = new UserRepository(filePath);
        }

        public User? Login(string username, string password)
        {
            return _repo.GetUser(username, password);
        }
    }
}
