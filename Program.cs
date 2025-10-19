using System;
using System.Collections.Generic;
using System.Linq;
using BankingSystem;

public class Program
{
    private static Bank? bank;
    private static User? currentUser;

    public static void Main(string[] args)
    {
        bank = new Bank();
        Console.WriteLine("Welcome to the MarvinBank");

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
            if (bank != null && account != null)
            {
                try
                {
                    bank.OpenAccount(currentUser, account);
                    Console.WriteLine("Account opened successfully!");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid deposit amount.");
        }
    }

    private static Account? SelectAccount()
    {
        var accounts = bank?.GetAccounts(currentUser);
        if (accounts == null || !accounts.Any())
        {
            Console.WriteLine("No accounts found. Please open one first.");
            return null;
        }

        if (accounts.Count == 1)
        {
            return accounts[0];
        }

        Console.WriteLine("Select an account:");
        for (int i = 0; i < accounts.Count; i++)
        {
            var accountType = accounts[i] is SavingsAccount ? "Savings" : "Checking";
            Console.WriteLine($"{i + 1}. {accountType} Account - Balance: R{accounts[i].Balance:F2}");
        }

        Console.Write("Choose an account: ");
        if (int.TryParse(Console.ReadLine(), out int accountIndex) && accountIndex > 0 && accountIndex <= accounts.Count)
        {
            var selectedAccount = accounts[accountIndex - 1];
            Console.WriteLine($"Selected Account: {selectedAccount.AccountNumber}");

            string transactionPrompt = "Choose transaction: (1) Deposit (2) Withdraw (3) Check Balance";
            if (selectedAccount is CreditAccount)
            {
                transactionPrompt = "Choose transaction: (1) Make Payment (2) Withdraw (3) Check Balance";
            }
            Console.WriteLine(transactionPrompt);

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Enter amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                    {
                        if (selectedAccount is CreditAccount creditAccount)
                        {
                            creditAccount.MakePayment(depositAmount);
                        }
                        else
                        {
                            selectedAccount.Deposit(depositAmount);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid deposit amount.");
                    }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private static void CheckBalance()
    {
        var accounts = bank?.GetAccounts(currentUser);
        if (accounts != null && accounts.Any())
        {
            Console.WriteLine("\n--- Account Balances ---");
            foreach (var acc in accounts)
            {
                acc.CheckBalance();
            }
            Console.WriteLine("------------------------");
        }
        else
        {
            Console.WriteLine("No accounts found.");
        }
    }
}