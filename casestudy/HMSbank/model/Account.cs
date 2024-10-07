using System;

namespace HMSbank.model
{
    public class Account
    {
        // Confidential attributes
        private int accountNumber;
        private string accountType;
        private double accountBalance;
        private const double interestRate = 4.5;
        private int customerId; // New field for customer ID

        // Default constructor
        public Account() { }

        // Overloaded constructor with 4 arguments
        public Account(int accountNumber, string accountType, double accountBalance, int customerId)
        {
            this.accountNumber = accountNumber;
            this.accountType = accountType;
            this.accountBalance = accountBalance;
            this.customerId = customerId;
        }

        // Getters and Setters
        public int AccountNumber { get => accountNumber; set => accountNumber = value; }
        public string AccountType { get => accountType; set => accountType = value; }
        public double AccountBalance { get => accountBalance; set => accountBalance = value; }
        public int CustomerId { get => customerId; set => customerId = value; } // Getter/Setter for customerId

        // Method to deposit
        public void Deposit(double amount)
        {
            accountBalance += amount;
            Console.WriteLine($"Deposited: ${amount}. New Balance: ${accountBalance}");
        }

        // Method to withdraw
        public void Withdraw(double amount)
        {
            if (amount <= accountBalance)
            {
                accountBalance -= amount;
                Console.WriteLine($"Withdrawn: ${amount}. New Balance: ${accountBalance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        // Method to calculate interest
        public void CalculateInterest()
        {
            double interest = accountBalance * (interestRate / 100);
            accountBalance += interest;
            Console.WriteLine($"Interest: ${interest}. New Balance: ${accountBalance}");
        }

        // Method to print account info
        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Account Type: {accountType}");
            Console.WriteLine($"Account Balance: ${accountBalance}");
            Console.WriteLine($"Customer ID: {customerId}"); // Display customerId
        }
    }
}
