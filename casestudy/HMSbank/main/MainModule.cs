using System;
using System.Data.SqlClient;
using HMSbank.util;
using HMSbank.model;
using BankApp.util;
using HMSbank.dao;

namespace HMSbank.main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            // Get the connection string from appsettings.json
            string connectionString = DBConnUtil.GetConnectionString("appsettings.json");

            // Establish a connection to the database
            SqlConnection conn = DBConnUtil.GetConnection(connectionString);

            // Create an AccountService object
            IAccountService accountService = new AccountServiceImpl(conn);

            // Welcome message
            Console.WriteLine("Welcome to HMS Bank!");

            // Input: Customer details from user
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            // Create a new customer
            accountService.CreateCustomer(firstName, lastName, email, phoneNumber, address);

            // Input: Account details from user
            Console.Write("Enter Account Number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter Account Type (Savings/Current): ");
            string accountType = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            decimal initialBalance = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Customer ID for the account: ");
            int customerId = int.Parse(Console.ReadLine());

            // Create a new account
            accountService.CreateAccount(accountNumber, accountType, initialBalance, customerId);

            // Example operations
            Console.Write("Enter Account Number to Deposit: ");
            int depositAccountNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter Amount to Deposit: ");
            decimal depositAmount = decimal.Parse(Console.ReadLine());
            accountService.Deposit(depositAccountNumber, depositAmount);

            Console.Write("Enter Account Number to Withdraw: ");
            int withdrawAccountNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter Amount to Withdraw: ");
            decimal withdrawAmount = decimal.Parse(Console.ReadLine());
            accountService.Withdraw(withdrawAccountNumber, withdrawAmount);

            Console.Write("Enter Source Account Number for Transfer: ");
            int fromAccountNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter Destination Account Number for Transfer: ");
            int toAccountNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter Amount to Transfer: ");
            decimal transferAmount = decimal.Parse(Console.ReadLine());
            accountService.Transfer(fromAccountNumber, toAccountNumber, transferAmount);

            // Display Account and Customer details
            Console.Write("Enter Account Number to Display: ");
            int displayAccountNumber = int.Parse(Console.ReadLine());
            accountService.DisplayAccountDetails(displayAccountNumber);

            Console.Write("Enter Customer ID to Display: ");
            int displayCustomerId = int.Parse(Console.ReadLine());
            accountService.DisplayCustomerDetails(displayCustomerId);

            // Close the connection
            conn.Close();
            Console.WriteLine("Connection closed.");
        }
    }
}
