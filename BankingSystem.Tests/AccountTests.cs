using Xunit;
using BankingSystem;

namespace BankingSystem.Tests
{
    
    public class TestAccount : Account
    {
        public TestAccount(string ownerName, decimal initialBalance = 0) : base(ownerName, initialBalance) { }

        public override decimal CalculateInterest()
        {
            return 0m; 
        }
    }

    public class AccountTests
    {
        [Fact]
        public void Deposit_WithValidAmount_IncreasesBalance()
        {
            
            var account = new TestAccount("Nesh Marvin", 100m);

            
            account.Deposit(50m);

            
            Assert.Equal(150m, account.Balance);
        }
    }
}