using System;

namespace BankingSystem
{
    public class BusinessAccount : Account
    {
        private const decimal TransactionFee = 2.50m; // A flat fee for each withdrawal
        public decimal OverdraftLimit { get; }

        public BusinessAccount(string ownerName, decimal initialBalance, decimal overdraftLimit) : base(ownerName, initialBalance)
        {
            if (overdraftLimit < 0)
            {
                throw new ArgumentException("Overdraft limit cannot be negative.");
            }
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }

            decimal totalDeduction = amount + TransactionFee;

            if (Balance - totalDeduction < -OverdraftLimit)
            {
                throw new InvalidOperationException("Withdrawal exceeds overdraft limit.");
            }

            Balance -= totalDeduction;
            Console.WriteLine($"Withdrew R{amount:F2} with a R{TransactionFee:F2} fee. New balance: R{Balance:F2}");
        }

        public override decimal CalculateInterest()
        {
            // Business accounts typically do not earn interest in this model.
            return 0m;
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Business Account Balance: R{Balance:F2}. Overdraft Limit: R{OverdraftLimit:F2}.");
        }
    }
}