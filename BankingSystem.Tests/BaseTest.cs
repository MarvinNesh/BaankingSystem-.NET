using BankingSystem;

namespace BankingSystem.Tests
{
    public abstract class BaseTest : IDisposable
    {
        private bool disposed = false;

        protected BaseTest()
        {
            BeforeEach();  // Setup before each test
        }

        protected virtual void BeforeEach()
        {
            User.ClearRegistry();  // Clear static list for isolation
        }

        protected virtual void AfterEach()
        {
            // Cleanup if needed
        }

        public void Dispose()
        {
            if (!disposed)
            {
                AfterEach();  // Teardown after each test
                disposed = true;
            }
        }
    }
}