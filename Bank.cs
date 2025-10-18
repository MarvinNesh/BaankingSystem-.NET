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

      
    }
}