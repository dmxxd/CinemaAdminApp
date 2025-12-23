using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public class UserManager
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public User Authenticate(string login, string password)
        {
            string hashedPassword = HashPassword(password);
            return _users.FirstOrDefault(u => u.Username.Equals(login, System.StringComparison.OrdinalIgnoreCase) && u.HashedPassword == hashedPassword);
        }

        public bool AddUser(string username, string password, string role)
        {
            if (_users.Any(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            User newUser = new User
            {
                Id = _nextId++,
                Username = username,
                HashedPassword = HashPassword(password),
                Role = role
            };

            _users.Add(newUser);
            return true;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public bool UpdateUserRole(int userId, string newRole)
        {
            User user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Role = newRole;
                return true;
            }
            return false;
        }

        public bool DeleteUser(int userId)
        {
            User user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _users.Remove(user);
                return true;
            }
            return false;
        }
    }
}