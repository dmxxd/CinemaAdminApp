using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CinemaAdminApp.Data;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public class PersonalManager
    {
        private List<Personal> _personal = DataAccess.LoadPersonal();

        public Personal Authenticate(string login, string password)
        {
            var user = _personal.FirstOrDefault(p => p.Login == login);
            if (user != null && VerifyPasswordHash(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
        public List<Personal> GetAllPersonal() => _personal;
        public bool CreatePersonal(Personal newPersonal)
        {
            if (_personal.Any(p => p.Login == newPersonal.Login))
            {
                return false;
            }
            newPersonal.Id = _personal.Any() ? _personal.Max(p => p.Id) + 1 : 1;

            if (string.IsNullOrWhiteSpace(newPersonal.Login))
            {
                newPersonal.Login = newPersonal.FullName.Replace(" ", "").ToLowerInvariant();
            }
            if (string.IsNullOrWhiteSpace(newPersonal.PasswordHash))
            {
                newPersonal.PasswordHash = HashPassword("temp_pass_123");
            }

            _personal.Add(newPersonal);
            DataAccess.SavePersonal(_personal);
            return true;
        }
        public bool UpdatePersonal(Personal updatedPersonal)
        {
            var existing = _personal.FirstOrDefault(p => p.Id == updatedPersonal.Id);
            if (existing == null)
            {
                return false;
            }
            if (updatedPersonal.Login != existing.Login && _personal.Any(p => p.Login == updatedPersonal.Login))
            {
                return false;
            }
            existing.FullName = updatedPersonal.FullName;
            existing.Position = updatedPersonal.Position;
            existing.HourlyRate = updatedPersonal.HourlyRate;
            existing.Role = updatedPersonal.Role;
            existing.Login = updatedPersonal.Login;

            if (!string.IsNullOrWhiteSpace(updatedPersonal.PasswordHash) && updatedPersonal.PasswordHash != existing.PasswordHash)
            {
                existing.PasswordHash = HashPassword(updatedPersonal.PasswordHash);
            }

            DataAccess.SavePersonal(_personal);
            return true;
        }
        public bool DeletePersonal(int id)
        {
            var existing = _personal.FirstOrDefault(p => p.Id == id);
            if (existing == null)
            {
                return false;
            }

            _personal.Remove(existing);
            DataAccess.SavePersonal(_personal);
            return true;
        }
    }
}