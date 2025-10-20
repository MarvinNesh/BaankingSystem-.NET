# BankingSystem-.NET

A simple console-based banking application built with .NET. It allows users to register, log in, open different types of accounts, perform transactions, and transfer money to other users.

## Features

*   **User Authentication:** Secure registration and login with password hashing.
*   **Multiple Account Types:**
    *   Savings Account
    *   Checking Account
    *   Credit Account (with credit limit)
    *   Business Account (with overdraft limit)
*   **Account Management:**
    *   Open new accounts.
    *   Check the balance of all accounts.
*   **Transactions:**
    *   Deposit/Make Payment.
    *   Withdraw/Spend.
*   **User-to-User Transfers:** Transfer funds to any other user in the system using their account number.
*   **Data Persistence:** Uses Entity Framework Core with a SQLite database to store user and account information.

## Getting Started

### Prerequisites

*   [.NET SDK](https://dotnet.microsoft.com/download)

### Installation & Running

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    cd BaankingSystem-.NET
    ```

2.  **Restore dependencies:**
    The .NET CLI will automatically restore packages when you build or run the project.

3.  **Apply database migrations:**
    The application is configured to automatically apply pending migrations on startup.

4.  **Run the application:**
    ```bash
    dotnet run
    ```

## Technologies Used

*   **.NET:** The core framework for the application.
*   **Entity Framework Core:** For data access and object-relational mapping (ORM).
*   **SQLite:** As the database provider for Entity Framework Core.
*   **BCrypt.Net:** For hashing and verifying user passwords.