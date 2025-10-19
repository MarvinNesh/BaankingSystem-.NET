using Xunit;
using BankingSystem;
using System;
using System.IO;

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

        [Fact]
        public void Deposit_WithInvalidAmount_ThrowsArgumentException()
        {
            var account = new TestAccount("Nesh Marvin", 100m);

            var ex = Assert.Throws<ArgumentException>(() => account.Deposit(-50m));
            Assert.Contains("Deposit amount must be positive", ex.Message);
        }

        [Fact]
        public void Withdraw_WithValidAmount_DecreasesBalance()
        {
            var account = new TestAccount("Nesh Marvin", 100m);

            account.Withdraw(50m);

            Assert.Equal(50m, account.Balance);
        }

        [Fact]
        public void Withdraw_WithInsufficientFunds_ThrowsInvalidOperationException()
        {
            var account = new TestAccount("Nesh Marvin", 100m);

            var ex = Assert.Throws<InvalidOperationException>(() => account.Withdraw(150m));
            Assert.Contains("Insufficient funds", ex.Message);
        }

        [Fact]
        public void CheckBalance_DisplaysCorrectBalance()
        {
            var account = new TestAccount("Nesh Marvin", 100m);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            account.CheckBalance();

            var output = stringWriter.ToString();
            Assert.Contains("Balance for Nesh Marvin: R100.00", output);
        }

        [Fact]
        public void Constructor_SetsAccountNumberCorrectly()
        {
            var account = new TestAccount("Nesh Marvin", 100m);

            Assert.NotNull(account.AccountNumber);
            Assert.NotEmpty(account.AccountNumber);
            Assert.Contains("-", account.AccountNumber);
        }
        [Fact]
    public void SavingsAccount_WithdrawalBelowMinBalance_WarnsButWithdraws()
    {
        
        var account = new SavingsAccount("Nesh Marvin", 50m);  
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        
        account.Withdraw(20m);

        
        var output = stringWriter.ToString();
        Assert.Contains("Warning: Balance below R100", output);
        Assert.Equal(30m, account.Balance);  
    }
        [Fact]
    public void SavingsAccount_Constructor_InheritsBaseProperties()
    {
        
        var account = new SavingsAccount("Nesh Marvin", 100m);

        
        Assert.Equal("Nesh Marvin", account.OwnerName);
        Assert.Equal(100m, account.Balance);
        Assert.NotNull(account.AccountNumber);
    }

    [Fact]
    public void SavingsAccount_CalculateInterest_ReturnsThreePercent()
    {
        
        var account = new SavingsAccount("Nesh Marvin", 1000m);

        
        var interest = account.CalculateInterest();

        
        Assert.Equal(30m, interest);  // 1000 * 0.03 = 30
    }
        [Fact]
    public void SavingsAccount_ApplyInterest_IncreasesBalance()
    {
        
        var account = new SavingsAccount("Nesh Marvin", 1000m);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        
        account.ApplyInterest();

        
        var output = stringWriter.ToString();
        Assert.Contains("Applied interest: R30.00", output);  // 1000 * 0.03 = 30
        Assert.Equal(1030m, account.Balance);  // Balance + interest
    }
    
    }
}