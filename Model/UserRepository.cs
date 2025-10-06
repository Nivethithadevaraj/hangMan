using System;
using System.IO;
using System.Linq;

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
                if (raw.StartsWith("username", StringComparison.OrdinalIgnoreCase)) continue; // skip header

                var parts = raw.Split(',');
                if (parts.Length < 3) continue;

                var u = parts[0].Trim();
                var p = parts[1].Trim();
                var r = parts[2].Trim();

                // Case-sensitive check
                if (u == username && p == password)
                {
                    return new User(u, p, r);
                }
            }

            return null;
        }

        // Add a new user, or if username exists, auto-login with existing one
        public User? AddUser(User user)
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "username,password,role" + Environment.NewLine);
            }

            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("username", StringComparison.OrdinalIgnoreCase)) continue;

                var parts = line.Split(',');
                if (parts.Length < 3) continue;

                var existingUsername = parts[0].Trim();
                var existingPassword = parts[1].Trim();
                var existingRole = parts[2].Trim();

                // Case-sensitive duplicate check
                if (existingUsername == user.Username)
                {
                    Console.WriteLine($"User '{user.Username}' is already registered. Logging you in...");
                    return new User(existingUsername, existingPassword, existingRole); // auto-login
                }
            }

            // Otherwise append new user
            File.AppendAllText(_filePath, $"{user.Username},{user.Password},{user.Role}{Environment.NewLine}");
            Console.WriteLine("Registration successful! Logging you in...");
            return user;
        }
    }
}
