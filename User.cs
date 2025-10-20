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
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public byte[]? PasswordHash { get; private set; }
        public byte[]? PasswordSalt { get; private set; }
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

        private User() { }

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
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            }
        }

        public bool VerifyPassword(string password)
        {
            if (PasswordHash == null || PasswordSalt == null)
            {
                return false;
            }
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
    }
}