using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RestaurantManagementSystem_2_.Models
{
    public static class UserStore
    {
        private static List<User> _users = new List<User>();
        private static readonly string _filePath = "users.json";

        static UserStore()
        {
            LoadUsers();
        }

        public static bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username))
                return false;

            var newUser = new User { Username = username, Password = password };
            _users.Add(newUser);
            SaveUsers();
            return true;
        }

        public static User Login(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public static User GetUser(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public static List<User> GetAllUsers()
        {
            return _users;
        }

        private static void SaveUsers()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(_filePath, json);
        }

        private static void LoadUsers()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
                SaveUsers();
            }
        }
    }
    
}