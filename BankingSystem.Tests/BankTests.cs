using Xunit;
using BankingSystem;
using System.Linq;

namespace BankingSystem.Tests
{
    public class BankTests
    {
        [Fact]
        public void RegisterUser_WithValidData_AddsUser()
        {
            User.ClearRegistry();  // Clear for isolation
            var bank = new Bank();

            bank.RegisterUser("Nesh Marvin", "nesh@test.com", "secret123");

            var user = bank.Login("nesh@test.com", "secret123");
            Assert.NotNull(user);
        }

        [Fact]
        public void Login_WithValidData_ReturnsUser()
        {
            User.ClearRegistry();  // Clear for isolation
            var bank = new Bank();
            bank.RegisterUser("Nesh Marvin", "nesh@test.com", "secret123");

            var user = bank.Login("nesh@test.com", "secret123");

            Assert.Equal("Nesh Marvin", user.Name);
            Assert.Equal("nesh@test.com", user.Email);
        }
    }
}