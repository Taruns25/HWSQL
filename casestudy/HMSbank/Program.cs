//task 1
/*using System;

class LoanEligibility
{
    static void Main(string[] args)
    {
        // Take input for credit score and annual income
        Console.Write("Enter Credit Score: ");
        int creditScore = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Annual Income: $");
        double annualIncome = Convert.ToDouble(Console.ReadLine());

        // Check eligibility criteria
        if (creditScore > 700 && annualIncome >= 50000)
        {
            Console.WriteLine("Congratulations! You are eligible for a loan.");
        }
        else if (creditScore <= 700)
        {
            Console.WriteLine("Sorry, you are not eligible for a loan. Your credit score is too low.");
        }
        else if (annualIncome < 50000)
        {
            Console.WriteLine("Sorry, you are not eligible for a loan. Your annual income is too low.");
        }
    }
}*/

// task 2
/*using System;

class ATMTransaction
{
    static void Main(string[] args)
    {
        // Take input for the user's initial balance
        Console.Write("Enter your current balance: $");
        double balance = Convert.ToDouble(Console.ReadLine());

        // Display ATM options
        Console.WriteLine("\nATM Menu:");
        Console.WriteLine("1. Check Balance");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Deposit");
        Console.Write("Choose an option (1/2/3): ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                // Check Balance
                Console.WriteLine($"\nYour current balance is: ${balance}");
                break;

            case 2:
                // Withdraw
                Console.Write("\nEnter the amount to withdraw (multiples of 100 or 500): $");
                double withdrawAmount = Convert.ToDouble(Console.ReadLine());

                // Check if withdrawal amount is valid
                if (withdrawAmount <= balance)
                {
                    if (withdrawAmount % 100 == 0 || withdrawAmount % 500 == 0)
                    {
                        balance -= withdrawAmount;
                        Console.WriteLine($"\nWithdrawal successful! Your new balance is: ${balance}");
                    }
                    else
                    {
                        Console.WriteLine("\nError: Withdrawal amount must be in multiples of 100 or 500.");
                    }
                }
                else
                {
                    Console.WriteLine("\nError: Insufficient balance for this transaction.");
                }
                break;

            case 3:
                // Deposit
                Console.Write("\nEnter the amount to deposit: $");
                double depositAmount = Convert.ToDouble(Console.ReadLine());

                // Add deposit amount to the balance
                balance += depositAmount;
                Console.WriteLine($"\nDeposit successful! Your new balance is: ${balance}");
                break;

            default:
                // Invalid option
                Console.WriteLine("\nError: Invalid option selected.");
                break;
        }
    }
}
*/

//task 3
/*
using System;
class CompoundInterest
{
    static void Main(string[] args)
    {
        
        Console.Write("Enter the number of customers: ");
        int customerCount = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= customerCount; i++)
        {
            Console.WriteLine($"\nCustomer {i}:");
           
            Console.Write("Enter initial balance: $");
            double initialBalance = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter annual interest rate (in %): ");
            double annualInterestRate = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter the number of years: ");
            int years = Convert.ToInt32(Console.ReadLine());

            //  compound interest formula
            double futureBalance = initialBalance * Math.Pow((1 + annualInterestRate / 100), years);

            Console.WriteLine($"Future balance after {years} years: ${futureBalance:F2}");
        }
    }
}
*/

//task 4
/*
using System;
class BankSystem
{
    static void Main(string[] args)
    {
       
        int[] accountNumbers = { 1001, 1002, 1003, 1004, 1005 }; // ex for Valid account numbers
        double[] accountBalances = { 1500.75, 2340.50, 1290.00, 875.25, 5690.00 }; // Corresponding balances

        bool validAccount = false;

        while (!validAccount)
        {
            Console.Write("Enter your account number: ");
            int enteredAccountNumber = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < accountNumbers.Length; i++)
            {
                if (enteredAccountNumber == accountNumbers[i])
                {
                    Console.WriteLine($"Account Number: {enteredAccountNumber}");
                    Console.WriteLine($"Balance: ${accountBalances[i]:F2}");
                    validAccount = true; // Set flag to true to exit the loop
                    break;
                }
            }
            if (!validAccount)
            {
                Console.WriteLine("Invalid account number. Please try again.");
            }
        }
    }
}
*/

//task 5
/*
using System;
class PasswordValidation
{
    static bool HasUppercase(string password)
    {
        foreach (char c in password)
        {
            if (char.IsUpper(c)) return true;
        }
        return false;
    }
    static bool HasDigit(string password)
    {
        foreach (char c in password)
        {
            if (char.IsDigit(c)) return true;
        }
        return false;
    }
    static void Main(string[] args)
    {
        
        Console.Write("Create your password: ");
        string password = Console.ReadLine();

        if (password.Length >= 8 && HasUppercase(password) && HasDigit(password))
        {
            Console.WriteLine("Password is valid!");
        }
        else
        {
            Console.WriteLine("Invalid password. Make sure it is at least 8 characters long, contains at least one uppercase letter, and one digit.");
        }
    }
}
*/

//task 6
/*
using System;
using System.Collections.Generic;
class BankTransactions
{
    static void Main(string[] args)
    {
        List<string> transactionHistory = new List<string>();
        double balance = 0.0;
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nBank Transaction Menu:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Exit and Display balance");
            Console.Write("Choose an option (1/2/3): ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    // Deposit 
                    Console.Write("Enter deposit amount: $");
                    double depositAmount = Convert.ToDouble(Console.ReadLine());
                    balance += depositAmount;
                    transactionHistory.Add($"Deposit: +${depositAmount:F2}, New Balance: ${balance:F2}");
                    Console.WriteLine($"Deposit successful! New balance: ${balance:F2}");
                    break;

                case 2:
                    // Withdraw 
                    Console.Write("Enter withdrawal amount: $");
                    double withdrawAmount = Convert.ToDouble(Console.ReadLine());

                    if (withdrawAmount <= balance)
                    {
                        balance -= withdrawAmount;
                        Console.WriteLine($"Withdrawal successful! New balance: ${balance:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Insufficient balance for this withdrawal.");
                    }
                    break;

                case 3:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        Console.WriteLine($"\nFinal Balance: ${balance:F2}");
    }
}
*/
