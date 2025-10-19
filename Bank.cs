using System;
using System.Collections.Generic;

namespace BankingSystem
{
    public class Bank
    {
        private List<User> users = new List<User>();
        private Dictionary<User, List<Account>> userAccounts = new Dictionary<User, List<Account>>();

        public void RegisterUser(string name, string email, string password)
        {
            var user = new User(name, email);
            user.SetPassword(password);
            users.Add(user);
            userAccounts[user] = new List<Account>();
            Console.WriteLine($"Registered user: {name}");
        }

        public User Login(string email, string password)
        {
            var user = users.Find(u => u.Email == email);
            if (user != null && user.VerifyPassword(password))
            {
                return user;
            }
            throw new InvalidOperationException("Invalid credentials.");
        }
    }
}