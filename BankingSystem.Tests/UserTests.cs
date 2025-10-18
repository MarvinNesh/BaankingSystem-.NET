using Xunit;
using BankingSystem;

namespace BankingSystem.Tests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_WithValidData_SetsProperties()
        {
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
            var user = new User("Nesh Marvin", "nesh@test.com");
            var expectedHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("secret123bankSalt"));

            user.SetPassword("secret123");

            Assert.NotEqual("secret123", user.Password);
            Assert.Equal(expectedHash, user.Password);
        }
    }
}