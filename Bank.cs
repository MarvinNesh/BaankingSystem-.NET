using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingSystem
{
    public class Bank
    {
        private readonly Dictionary<string, List<Account>> _accounts = new Dictionary<string, List<Account>>();

        public void RegisterUser(string name, string email, string password)
        {
            User.Register(name, email, password);
        }

        public User? Login(string email, string password)
        {
            var user = User.FindByCredentials(email, password);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid credentials.");
            }
            return user;
        }

        public void OpenAccount(User user, Account newAccount)
        {
            if (user == null) return;

            if (!_accounts.ContainsKey(user.Email))
            {
                _accounts[user.Email] = new List<Account>();
            }

            var existingAccounts = _accounts[user.Email];
            if (existingAccounts.Any(acc => acc.GetType() == newAccount.GetType()))
            {
                string accountTypeName = newAccount.GetType().Name.Replace("Account", "");
                throw new InvalidOperationException($"You already have a {accountTypeName} account.");
            }
            
            existingAccounts.Add(newAccount);
        }

        public List<Account>? GetAccounts(User? user)
        {
            if (user != null)
            {
                return _accounts.GetValueOrDefault(user.Email);
            }
            return null;
        }
    }
}