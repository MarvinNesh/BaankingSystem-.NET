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

        public void Transfer(Account fromAccount, Account toAccount, decimal amount)
        {
            if (fromAccount == null || toAccount == null)
            {
                throw new ArgumentNullException("Accounts cannot be null.");
            }

            // This will throw an exception if funds are insufficient, stopping the transaction.
            fromAccount.Withdraw(amount);

            try
            {
                // Only if the withdrawal is successful, we attempt the deposit.
                toAccount.Deposit(amount);
            }
            catch (Exception)
            {
                // If the deposit fails, we roll back the successful withdrawal.
                fromAccount.Deposit(amount);
                throw new InvalidOperationException("Transfer failed. The transaction has been rolled back.");
            }
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