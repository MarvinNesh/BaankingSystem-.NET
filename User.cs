using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BankingSystem
{
    public class User
    {
        private static List<User> existingUsers = new List<User>();

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Password { get; private set; }

        public User(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "User name cannot be null or empty.");
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.", nameof(email));
            }

            Name = name;
            Email = email;
        }

        public void SetPassword(string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Password cannot be empty.", nameof(newPassword));
            }
            Password = SimpleHash.Compute(newPassword);
        }

        public bool VerifyPassword(string password)
        {
            return Password == SimpleHash.Compute(password);
        }

        public static void Register(string name, string email, string password)
        {
            if (existingUsers.Any(u => u.Email == email))
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new User(name, email);
            user.SetPassword(password);
            existingUsers.Add(user);
        }

        public static User? FindByCredentials(string email, string password)
        {
            var user = existingUsers.FirstOrDefault(u => u.Email == email);
            if (user != null && user.VerifyPassword(password))
            {
                return user;
            }
            return null;
        }

        public static void ClearRegistry()
        {
            existingUsers.Clear();
        }
    }
}