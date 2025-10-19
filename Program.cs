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
        Console.WriteLine("1. Open Account\n2. Perform Transaction\n3. Check All Balances\n4. Logout");
        Console.Write("Choose an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                OpenAccount();
                break;
            case "2":
                PerformTransactions();
                break;
            case "3":
                CheckAllBalances();
                break;
            case "4":
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

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                bank?.RegisterUser(name, email, password);
                Console.WriteLine("Registration successful!");
            }
            else
            {
                Console.WriteLine("Name, email, and password cannot be empty.");
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

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
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

        Console.WriteLine("Choose account type:\n1. Savings\n2. Checking\n3. Credit");
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
                case "3":
                    Console.Write("Enter credit limit: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal creditLimit))
                    {
                        account = new CreditAccount(currentUser.Name, creditLimit, initialDeposit);
                    }
                    else
                    {
                        Console.WriteLine("Invalid credit limit amount.");
                        return;
                    }
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

        Console.WriteLine("\nSelect an account:");
        for (int i = 0; i < accounts.Count; i++)
        {
            string accountTypeName = accounts[i].GetType().Name.Replace("Account", "");
            Console.WriteLine($"{i + 1}. {accountTypeName}");
        }

        Console.Write("Choose an account: ");
        if (int.TryParse(Console.ReadLine(), out int accountIndex) && accountIndex > 0 && accountIndex <= accounts.Count)
        {
            return accounts[accountIndex - 1];
        }
        else
        {
            Console.WriteLine("Invalid selection.");
            return null;
        }
    }

    private static void PerformTransactions()
    {
        var selectedAccount = SelectAccount();
        if (selectedAccount == null)
        {
            return;
        }

        Console.WriteLine($"Selected Account: {selectedAccount.AccountNumber}");
        selectedAccount.CheckBalance();

        string transactionPrompt = "\nChoose transaction:\n1. Deposit\n2. Withdraw";
        if (selectedAccount is CreditAccount)
        {
            transactionPrompt = "\nChoose transaction:\n1. Make Payment\n2. Withdraw";
        }
        Console.WriteLine(transactionPrompt);
        Console.Write("Choose an option: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Enter amount: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    try
                    {
                        if (selectedAccount is CreditAccount creditAccount)
                        {
                            creditAccount.MakePayment(amount);
                        }
                        else
                        {
                            selectedAccount.Deposit(amount);
                        }
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
                break;
            case "2":
                Console.Write("Enter amount: ");
                if (decimal.TryParse(Console.ReadLine(), out amount))
                {
                    try
                    {
                        selectedAccount.Withdraw(amount);
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
                break;
            default:
                Console.WriteLine("Invalid transaction type.");
                break;
        }
    }

    private static void CheckAllBalances()
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