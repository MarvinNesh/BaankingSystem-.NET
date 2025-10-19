using System;

namespace BankingSystem
{
    public class CreditAccount : Account
    {
        public decimal CreditLimit { get; }
        private const decimal InterestRate = 0.20m; // 20% annual interest rate for outstanding balance

        public CreditAccount(string ownerName, decimal creditLimit, decimal initialBalance = 0) : base(ownerName, initialBalance)
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

            if (Balance - amount < -CreditLimit)
            {
                throw new InvalidOperationException("Withdrawal exceeds credit limit.");
            }

            Balance -= amount;
            Console.WriteLine($"Withdrew R{amount:F2}. New balance: R{Balance:F2}. Available credit: R{CreditLimit + Balance:F2}");
        }
        
        public void MakePayment(decimal amount)
        {
            Deposit(amount); // A payment is just a deposit.
        }

        public override decimal CalculateInterest()
        {
            if (Balance < 0)
            {
                // Calculate interest on the negative (borrowed) balance
                return -Balance * (InterestRate / 12); // Monthly interest
            }
            return 0m;
        }

        public void ApplyInterest()
        {
            decimal interest = CalculateInterest();
            if (interest > 0)
            {
                Balance -= interest; // Interest is a charge, so it decreases the balance further
                Console.WriteLine($"Applied interest charge: R{interest:F2}. New balance: R{Balance:F2}");
            }
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Credit Account Balance: R{Balance:F2}. Credit Limit: R{CreditLimit:F2}. Available Credit: R{CreditLimit + Balance:F2}");
        }
    }
}