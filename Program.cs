using System;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing user
            Console.WriteLine("Testing User class...");
            try
            {
                var user = new User("Marvin Nesh", "marvin@test.com");
                user.SetPassword("secret123");
                Console.WriteLine($"User: {user.Name}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Hashed Password: {user.Password}"); 
                Console.WriteLine("User test passed! Press Enter to exit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: {ex.Message}");
            }
            Console.ReadLine();
        }dotnet build
dotnet run
    }
}