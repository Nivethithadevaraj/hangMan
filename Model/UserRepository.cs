using System;
using System.IO;

namespace HangmanGame.Model
{
    public class UserRepository
    {
        private readonly string _filePath;

        public UserRepository(string filePath)
        {
            _filePath = filePath;
        }

        // Returns the user if username/password matches exactly (case-sensitive)
        public User? GetUser(string username, string password)
        {
            if (!File.Exists(_filePath)) return null;

            var lines = File.ReadAllLines(_filePath);
            foreach (var raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                if (raw.StartsWith("username", StringComparison.OrdinalIgnoreCase)) continue; // skip header
                var parts = raw.Split(',');
                if (parts.Length < 3) continue;

                var u = parts[0].Trim();
                var p = parts[1].Trim();
                var r = parts[2].Trim();

                // case-sensitive match
                if (u == username && p == password)
                {
                    return new User(u, p, r);
                }
            }

            return null;
        }

        // Appends a new user to the CSV
        public void AddUser(User user)
        {
            bool fileExists = File.Exists(_filePath);

            using (var sw = new StreamWriter(_filePath, append: true))
            {
                if (!fileExists)
                {
                    sw.WriteLine("username,password,role"); // header if new file
                }
                sw.WriteLine($"{user.Username},{user.Password},{user.Role}");
            }
        }
    }
}
