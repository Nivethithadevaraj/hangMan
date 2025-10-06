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

        // Returns the user if username/password matches; expects CSV lines: username,password,role (header OK)
        public User? GetUser(string username, string password)
        {
            if (!File.Exists(_filePath)) return null;

            var lines = File.ReadAllLines(_filePath);
            foreach (var raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                if (raw.ToLower().StartsWith("username")) continue; // skip header
                var parts = raw.Split(',');
                if (parts.Length < 3) continue;

                var u = parts[0].Trim();
                var p = parts[1].Trim();
                var r = parts[2].Trim();

                if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase) && p == password)
                {
                    return new User(u, p, r);
                }
            }

            return null;
        }
    }
}
