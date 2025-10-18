using System;
using System.Linq; 

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Temporary test for User class
            Console.WriteLine("Testing User class...");
            try
            {
                var user = new User("Marvin Nesh", "marvin@test.com");
                user.SetPassword("secret123");
                Console.WriteLine($"User: {user.Name}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Hashed Password: {user.Password}"); // hashed password
                Console.WriteLine("User creation test passed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: {ex.Message}");
            }

            // Duplicate email test
            Console.WriteLine("\nTesting duplicate email...");
            try
            {
                var duplicateUser = new User("vhuhwavho Marvin", "marvin@test.com"); // Same email
                duplicateUser.SetPassword("anotherpass");
                Console.WriteLine("Duplicate test failed to catch error!"); // Should not reach here
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Duplicate test passed: {ex.Message}"); // Expects "Email already registered."
            }

            Console.WriteLine("\nAll tests complete. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}