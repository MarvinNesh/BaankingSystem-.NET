using System;
using System.Collections.Generic;

namespace BankingSystem
{
    public class Bank
    {
        private List<User> users = new List<User>();

        public void RegisterUser(string name, string email, string password)
        {
            //  Create user and add to list
            var user = new User(name, email);
            user.SetPassword(password);
            users.Add(user);
            Console.WriteLine($"Registered user: {name}");
        }

        public User Login(string email, string password)
        {
            //  Find user and verify password
            var user = users.Find(u => u.Email == email);
            if (user != null && password == user.Password) // Plain check for now (hashed later)
            {
                return user;
            }
            throw new InvalidOperationException("Invalid credentials.");
        }
    }
}