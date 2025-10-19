using System;

namespace BankingSystem
{
    public class CreditAccount : Account
    {
        public decimal CreditLimit { get; }
        private const decimal InterestRate = 0.20m; // 20% annual interest rate for outstanding balance

        public CreditAccount(string ownerName, decimal creditLimit, decimal initialPayment = 0) : base(ownerName, -initialPayment)
        {
            if (creditLimit <= 0)
            {
                throw new ArgumentException("Credit limit must be positive.");
            }
            CreditLimit = creditLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }

            if (Balance + amount > CreditLimit)
            {
                throw new InvalidOperationException("Withdrawal exceeds credit limit.");
            }

            Balance += amount;
            Console.WriteLine($"Spent R{amount:F2}. Amount owed: R{Balance:F2}. Available credit: R{CreditLimit - Balance:F2}");
        }
        
        public void MakePayment(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Payment amount must be positive.");
            }
            // A payment reduces the amount owed.
            Balance -= amount;
            Console.WriteLine($"Paid R{amount:F2}. New amount owed: R{Balance:F2}.");
        }

        public override void Deposit(decimal amount)
        {
            // Deposits on a credit account are treated as payments.
            MakePayment(amount);
        }

        public override decimal CalculateInterest()
        {
            if (Balance > 0)
            {
                // Calculate interest on the positive (borrowed) balance
                return Balance * (InterestRate / 12); // Monthly interest
            }
            return 0m;
        }

        public void ApplyInterest()
        {
            decimal interest = CalculateInterest();
            if (interest > 0)
            {
                Balance += interest; // Interest is a charge, so it increases the amount owed
                Console.WriteLine($"Applied interest charge: R{interest:F2}. New amount owed: R{Balance:F2}");
            }
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Credit Account - Amount Owed: R{Math.Max(0, Balance):F2}. Credit Limit: R{CreditLimit:F2}. Available Credit: R{CreditLimit - Balance:F2}");
            if (Balance < 0)
            {
                Console.WriteLine($"You have a credit of R{-Balance:F2}.");
            }
        }
    }
}