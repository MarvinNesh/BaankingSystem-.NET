using System;

namespace BankingSystem
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } 

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