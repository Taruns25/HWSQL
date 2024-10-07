using System;
using System.Data.SqlClient;
using HMSbank.dao;
using HMSbank.model;
using HMSbank.util;

namespace HMSbank.dao

{
    public class AccountServiceImpl : IAccountService
    {
        private SqlConnection _conn;

        public AccountServiceImpl(SqlConnection conn)
        {
            _conn = conn;
        }

        // Method to create a new customer
        public void CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string address)
        {
            string query = "INSERT INTO Customer (first_name, last_name, email, phone_number, address) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address)";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@Address", address);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Customer {firstName} {lastName} created successfully.");
        }

        // Method to create a new account
        public void CreateAccount(int accountNumber, string accountType, decimal initialBalance, int customerId)
        {
            string query = "INSERT INTO Account (account_number, account_type, balance, customer_id) VALUES (@AccountNumber, @AccountType, @Balance, @CustomerId)";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@AccountType", accountType);
                command.Parameters.AddWithValue("@Balance", initialBalance);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.ExecuteNonQuery();
            }

            Console.WriteLine($"Account {accountNumber} created successfully for customer ID {customerId}.");
        }

        // Method to deposit money into an account
        public void Deposit(int accountNumber, decimal amount)
        {
            decimal currentBalance = GetAccountBalance(accountNumber);
            decimal newBalance = currentBalance + amount;

            UpdateAccountBalance(accountNumber, newBalance);
            LogTransaction(accountNumber, "Deposit", amount);

            Console.WriteLine($"Amount deposited successfully. New balance: {newBalance}");
        }

        // Method to withdraw money from an account
        public void Withdraw(int accountNumber, decimal amount)
        {
            decimal currentBalance = GetAccountBalance(accountNumber);
            if (currentBalance < amount)
            {
                Console.WriteLine("Insufficient balance.");
                return;
            }

            decimal newBalance = currentBalance - amount;
            UpdateAccountBalance(accountNumber, newBalance);
            LogTransaction(accountNumber, "Withdrawal", amount);

            Console.WriteLine($"Amount withdrawn successfully. New balance: {newBalance}");
        }

        // Method to transfer money from one account to another
        public void Transfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            decimal fromAccountBalance = GetAccountBalance(fromAccountNumber);
            if (fromAccountBalance < amount)
            {
                Console.WriteLine("Insufficient balance in the source account.");
                return;
            }

            decimal toAccountBalance = GetAccountBalance(toAccountNumber);
            UpdateAccountBalance(fromAccountNumber, fromAccountBalance - amount);
            UpdateAccountBalance(toAccountNumber, toAccountBalance + amount);
            LogTransaction(fromAccountNumber, "Withdrawal", amount);
            LogTransaction(toAccountNumber, "Deposit", amount);

            Console.WriteLine($"Transfer successful. {amount} transferred from {fromAccountNumber} to {toAccountNumber}.");
        }

        // Method to get the account balance
        private decimal GetAccountBalance(int accountNumber)
        {
            string query = "SELECT balance FROM Account WHERE account_number = @AccountNumber";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                decimal balance = (decimal)command.ExecuteScalar();
                return balance;
            }
        }

        // Method to update the account balance in the database
        private void UpdateAccountBalance(int accountNumber, decimal newBalance)
        {
            string query = "UPDATE Account SET balance = @Balance WHERE account_number = @AccountNumber";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@Balance", newBalance);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.ExecuteNonQuery();
            }
        }

        // Method to log transactions in the Transactions table
        private void LogTransaction(int accountNumber, string transactionType, decimal amount)
        {
            string query = "INSERT INTO Transactions (account_number, transaction_type, amount) VALUES (@AccountNumber, @TransactionType, @Amount)";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@TransactionType", transactionType);
                command.Parameters.AddWithValue("@Amount", amount);
                command.ExecuteNonQuery();
            }
        }

        // Method to display account details
        public void DisplayAccountDetails(int accountNumber)
        {
            string query = "SELECT * FROM Account WHERE account_number = @AccountNumber";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Account Details:");
                        Console.WriteLine($"Account Number: {reader["account_number"]}");
                        Console.WriteLine($"Account Type: {reader["account_type"]}");
                        Console.WriteLine($"Balance: {reader["balance"]}");
                        Console.WriteLine($"Customer ID: {reader["customer_id"]}");
                    }
                    else
                    {
                        Console.WriteLine($"Account {accountNumber} not found.");
                    }
                }
            }
        }

        // Method to display customer details
        public void DisplayCustomerDetails(int customerId)
        {
            string query = "SELECT * FROM Customer WHERE customer_id = @CustomerId";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@CustomerId", customerId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Customer Details:");
                        Console.WriteLine($"Customer ID: {reader["customer_id"]}");
                        Console.WriteLine($"First Name: {reader["first_name"]}");
                        Console.WriteLine($"Last Name: {reader["last_name"]}");
                        Console.WriteLine($"Email: {reader["email"]}");
                        Console.WriteLine($"Phone Number: {reader["phone_number"]}");
                        Console.WriteLine($"Address: {reader["address"]}");
                    }
                    else
                    {
                        Console.WriteLine($"Customer ID {customerId} not found.");
                    }
                }
            }
        }
    }
}
