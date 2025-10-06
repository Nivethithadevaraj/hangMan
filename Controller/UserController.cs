using HangmanGame.Model;

namespace HangmanGame.Controller
{
    public class UserController
    {
        private readonly UserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        public User? Login(string username, string password)
        {
            return _userRepository.GetUser(username, password);
        }
    }
}
