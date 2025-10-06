using HangmanGame.Model;

namespace HangmanGame.Controller
{
    public class UserController
    {
        private readonly UserRepository userRepo;

        public UserController()
        {
            userRepo = new UserRepository();
        }

        public User? Login(string username, string password)
        {
            return userRepo.GetUser(username, password);
        }
    }
}
