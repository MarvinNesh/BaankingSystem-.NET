using System;

namespace BankingSystem
{
    public abstract class Account
    {
        public string AccountNumber { get; protected set; }
        public string OwnerName { get; protected set; }
        public decimal Balance { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        protected Account(string ownerName, decimal initialBalance = 0)
        {
            OwnerName = ownerName;
            Balance = initialBalance;
            CreatedDate = DateTime.Now;
            AccountNumber = GenerateAccountNumber();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.");
            Balance += amount;
            Console.WriteLine($"Deposited ${amount:F2}. New balance: ${Balance:F2}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
            if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount:F2}. New balance: ${Balance:F2}");
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Balance for {OwnerName}: ${Balance:F2}");
        }

        public abstract decimal CalculateInterest(); // To override in derived classes

        private static string GenerateAccountNumber()
        {
            Random rand = new Random();
            return $"{rand.Next(1000, 9999)}-{rand.Next(100000, 999999)}";
        }
    }
}