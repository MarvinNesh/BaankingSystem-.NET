using System;
using System.Collections.Generic;
using System.Linq;  // (for Any(), FirstOrDefault(), etc.)


namespace BankingSystem
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } 
        private static List<User> existingUsers = new List<User>(); //acting as user storage

        public User(string name, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email format.");
            
            // Check uniqueness
            if (existingUsers.Any(u => u.Email == email))
                throw new InvalidOperationException("Email already registered.");

            Email = email;
            existingUsers.Add(this); // Register 
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty.");
            Password = SimpleHash(password);
        }
        private string SimpleHash(string password)
        {
            
            string salt = "bankSalt"; 
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password + salt));
        
        }
    }
}