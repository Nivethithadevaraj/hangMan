using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame.Model
{
    public class UserRepository
    {
        private readonly string _filePath = Path.Combine("Data", "login.csv");

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            if (!File.Exists(_filePath))
            {
                Console.WriteLine("Login file not found!");
                return users;
            }

            var lines = File.ReadAllLines(_filePath).Skip(1); // skip header
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    users.Add(new User(parts[0], parts[1], parts[2]));
                }
            }

            return users;
        }

        public User? GetUser(string username, string password)
        {
            return GetAllUsers()
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
