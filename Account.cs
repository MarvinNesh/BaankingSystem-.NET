using System;

namespace BankingSystem
{
    public abstract class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; protected set; }
        public string OwnerName { get; protected set; }
        public decimal Balance { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        protected Account() { } // For EF Core

        public Account(string ownerName, decimal initialBalance)
        {
            OwnerName = ownerName;
            Balance = initialBalance;
            CreatedDate = DateTime.Now;
            AccountNumber = GenerateAccountNumber();
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.");
            Balance += amount;
            Console.WriteLine($"Deposited R{amount:F2}. New balance: R{Balance:F2}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
            if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
            Console.WriteLine($"Withdrew R{amount:F2}. New balance: R{Balance:F2}");
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Balance for {OwnerName}: R{Balance:F2}");
        }

        public virtual decimal CalculateInterest()
        {
            return 0;
        }

        private static string GenerateAccountNumber()
        {
            var rand = new Random();
            return $"{rand.Next(1000, 9999)}-{rand.Next(100000, 999999)}";
        }
    }
}