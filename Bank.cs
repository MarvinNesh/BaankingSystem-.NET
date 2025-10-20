using System;
using System.Collections.Generic;
using System.Linq;
using BankingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem
{
    public class Bank
    {
        private readonly BankingContext _context;

        public Bank(BankingContext context)
        {
            _context = context;
        }

        public void RegisterUser(string name, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new User(name, email);
            user.SetPassword(password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !user.VerifyPassword(password))
            {
                throw new InvalidOperationException("Invalid credentials.");
            }
            return user;
        }

        public void OpenAccount(User user, Account newAccount)
        {
            if (user == null) return;

            var existingAccounts = _context.Accounts.Where(a => a.UserId == user.Id).ToList();
            if (existingAccounts.Any(acc => acc.GetType() == newAccount.GetType()))
            {
                string accountTypeName = newAccount.GetType().Name.Replace("Account", "");
                throw new InvalidOperationException($"You already have a {accountTypeName} account.");
            }
            
            newAccount.UserId = user.Id;
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
        }

        public List<Account> GetAccounts(User? user)
        {
            if (user == null)
            {
                return new List<Account>();
            }
            return _context.Accounts.Where(a => a.UserId == user.Id).ToList();
        }

        public void Transfer(Account fromAccount, Account toAccount, decimal amount)
        {
            if (fromAccount == null || toAccount == null)
            {
                throw new ArgumentNullException("Accounts cannot be null.");
            }

            var from = _context.Accounts.Find(fromAccount.Id);
            var to = _context.Accounts.Find(toAccount.Id);

            if (from == null || to == null)
            {
                throw new InvalidOperationException("One or both accounts not found.");
            }

            from.Withdraw(amount);
            to.Deposit(amount);

            _context.SaveChanges();
        }

        public void Transfer(Account fromAccount, string toAccountNumber, decimal amount)
        {
            if (fromAccount == null)
            {
                throw new ArgumentNullException(nameof(fromAccount), "Source account cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(toAccountNumber))
            {
                throw new ArgumentException("Destination account number cannot be empty.", nameof(toAccountNumber));
            }

            var from = _context.Accounts.Find(fromAccount.Id);
            if (from == null)
            {
                throw new InvalidOperationException("Source account not found.");
            }

            if (from.AccountNumber == toAccountNumber)
            {
                throw new InvalidOperationException("Source and destination accounts cannot be the same.");
            }

            var to = _context.Accounts.FirstOrDefault(a => a.AccountNumber == toAccountNumber);
            if (to == null)
            {
                throw new InvalidOperationException("Destination account not found.");
            }

            from.Withdraw(amount);
            to.Deposit(amount);

            _context.SaveChanges();
        }
    }
}