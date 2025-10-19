using System;
using BankingSystem;

public class Program
{
    private static Bank? bank;
    private static User? currentUser;

    public static void Main(string[] args)
    {
        bank = new Bank();
        Console.WriteLine("Welcome to the Banking System!");

        while (true)
        {
            if (currentUser == null)
            {
                ShowAuthMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
    }

    private static void ShowAuthMenu()
    {
        Console.WriteLine("\n1. Register\n2. Login\n3. Exit");
        Console.Write("Choose an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Register();
                break;
            case "2":
                Login();
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private static void ShowMainMenu()
    {
        Console.WriteLine($"\nWelcome, {currentUser?.Name}!");
        Console.WriteLine("1. Open Account\n2. Deposit\n3. Withdraw\n4. Check Balance\n5. Logout");
        Console.Write("Choose an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                OpenAccount();
                break;
            case "2":
                Deposit();
                break;
            case "3":
                Withdraw();
                break;
            case "4":
                CheckBalance();
                break;
            case "5":
                currentUser = null;
                Console.WriteLine("Logged out successfully.");
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private static void Register()
    {
        try
        {
            Console.Write("Enter name: ");
            var name = Console.ReadLine();
            Console.Write("Enter email: ");
            var email = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            if (name != null && email != null && password != null)
            {
                bank?.RegisterUser(name, email, password);
                Console.WriteLine("Registration successful!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void Login()
    {
        try
        {
            Console.Write("Enter email: ");
            var email = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            if (email != null && password != null)
            {
                currentUser = bank?.Login(email, password);
                if (currentUser != null)
                {
                    Console.WriteLine("Login successful!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void OpenAccount()
    {
        if (currentUser == null) return;

        Console.WriteLine("Choose account type:\n1. Savings\n2. Checking");
        var type = Console.ReadLine();
        Account? account = null;

        Console.Write("Enter initial deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out var initialDeposit))
        {
            switch (type)
            {
                case "1":
                    account = new SavingsAccount(currentUser.Name, initialDeposit);
                    break;
                case "2":
                    account = new CheckingAccount(currentUser.Name, initialDeposit);
                    break;
                default:
                    Console.WriteLine("Invalid account type.");
                    return;
            }
            bank?.OpenAccount(currentUser, account);
            Console.WriteLine("Account opened successfully!");
        }
        else
        {
            Console.WriteLine("Invalid deposit amount.");
        }
    }

    private static void Deposit()
    {
        var account = bank?.GetAccount(currentUser);
        if (account == null)
        {
            Console.WriteLine("No account found. Please open one first.");
            return;
        }

        Console.Write("Enter amount to deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out var amount))
        {
            try
            {
                account.Deposit(amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private static void Withdraw()
    {
        var account = bank?.GetAccount(currentUser);
        if (account == null)
        {
            Console.WriteLine("No account found. Please open one first.");
            return;
        }

        Console.Write("Enter amount to withdraw: ");
        if (decimal.TryParse(Console.ReadLine(), out var amount))
        {
            try
            {
                account.Withdraw(amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private static void CheckBalance()
    {
        var account = bank?.GetAccount(currentUser);
        if (account != null)
        {
            account.CheckBalance();
        }
        else
        {
            Console.WriteLine("No account found.");
        }
    }
}