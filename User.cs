using System;

namespace BankingSystem
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } // TODO hash this


        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty.");
            Password = password; //TODO hash the password when you store
        }
    }
}