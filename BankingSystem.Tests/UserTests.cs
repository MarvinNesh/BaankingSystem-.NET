using Xunit;
using BankingSystem;

namespace BankingSystem.Tests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_WithValidData_SetsProperties()
        {
        User.ClearRegistry(); // Clear for isolation
            var user = new User("Nesh Marvin", "marvin@test.com");

            Assert.Equal("Nesh Marvin", user.Name);
            Assert.Equal("marvin@test.com", user.Email);
        }

        [Fact]
        public void Constructor_WithNullName_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new User(null, "test@test.com"));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void Constructor_WithInvalidEmail_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new User("Alice", ""));
            Assert.Contains("Invalid email format", ex.Message);
        }

        [Fact]
        public void SetPassword_WithValidData_HashesPassword()
        {
            var user = new User("Nesh Marvin", "nesh@marvin.com");
            var password = "secret123bank";

            user.SetPassword(password);

            Assert.NotNull(user.Password);
            Assert.NotEqual(password, user.Password);
            Assert.True(user.VerifyPassword(password));
        }

        [Fact]
        public void SetPassword_WithEmptyData_ThrowsArgumentException()
        {
            User.ClearRegistry();  // Clear for isolation
            var user = new User("Nesh Marvin", "nesh@test.com");

            var ex = Assert.Throws<ArgumentException>(() => user.SetPassword(""));
            Assert.Contains("Password cannot be empty", ex.Message);
        }
    }
}