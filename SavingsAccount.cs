using System;

namespace BankingSystem
{
    public class SavingsAccount : Account
    {
        private const decimal InterestRate = 0.03m; // 3% annual

        public SavingsAccount(string ownerName, decimal initialBalance = 0) : base(ownerName, initialBalance) { }

        public override void Withdraw(decimal amount)
        {
            if (Balance < 100m) // Min balance warning
            {
                Console.WriteLine("Warning: Balance below R100. Withdrawal allowed but fee applies next month.");
            }
            base.Withdraw(amount);
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Savings Account Balance: R{Balance:F2}");
        }

        public override decimal CalculateInterest()
        {
            return Balance * InterestRate; // Simple annual interest
        }

        public void ApplyInterest()
        {
            decimal interest = CalculateInterest();
            Deposit(interest);
            Console.WriteLine($"Applied interest: R{interest:F2}");
        }
    }
}