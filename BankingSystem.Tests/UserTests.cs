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
            var ex = Assert.Throws<ArgumentException>(() => new User("Marvin", ""));
            Assert.Contains("Invalid email format", ex.Message);
        }
    }
}