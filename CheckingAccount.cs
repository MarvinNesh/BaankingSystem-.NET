using System;

namespace BankingSystem
{
    public class CheckingAccount : Account
    {
        private const decimal OverdraftFee = 35.00m;

        public CheckingAccount(string ownerName, decimal initialBalance = 0) : base(ownerName, initialBalance) { }

        public override void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                decimal fee = OverdraftFee;
                Balance -= (amount + fee);
                Console.WriteLine($"Withdrew R{amount:F2} + R{fee:F2} overdraft fee. New balance: R{Balance:F2}");
                return;
            }
            base.Withdraw(amount);
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Checking Account Balance: R{Balance:F2}");
        }

        public override decimal CalculateInterest()
        {
            return 0m; // No interest on checking
        }
    }
}