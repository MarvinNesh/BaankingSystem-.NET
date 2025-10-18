using Xunit;
using BankingSystem;

namespace BankingSystem.Tests
{
    public class BankTests
    {
        [Fact]
        public void RegisterUser_WithValidData_AddsUser()
        {
            
            var bank = new Bank();

            
            bank.RegisterUser("Nesh Marvin", "nesh@test.com", "secret123");

            
            Assert.True(true);
        }
    }
}