using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using HMSbank.model;

namespace HMSbank.model
{
    public class Bank
    {
        private Dictionary<long, Account> accounts; // Stores account information
        private int nextAccountNumber;

        public Bank()
        {
            accounts = new Dictionary<long, Account>();
            nextAccountNumber = 1001; // Starting account number
        }

        // Method to create a new account
        public Account CreateAccount(Customer customer, string accType, float balance)
        {
            int accNo = nextAccountNumber++;
            Account newAccount = new Account(accNo, accType, balance, customer.CustomerId);
            accounts[accNo] = newAccount;
            return newAccount;
        }

        // Method to get account balance
        public float GetAccountBalance(long accountNumber)
        {
            if (accounts.ContainsKey(accountNumber))
                return (float)accounts[accountNumber].AccountBalance;
            throw new Exception("Account not found.");
        }

        // Method to deposit money
        public float Deposit(long accountNumber, float amount)
        {
            if (accounts.ContainsKey(accountNumber))
            {
                accounts[accountNumber].Deposit(amount);
                return (float) accounts[accountNumber].AccountBalance;
            }
            throw new Exception("Account not found.");
        }

        // Method to withdraw money
        public float Withdraw(long accountNumber, float amount)
        {
            if (accounts.ContainsKey(accountNumber))
            {
                accounts[accountNumber].Withdraw(amount);
                return (float) accounts[accountNumber].AccountBalance;
            }
            throw new Exception("Account not found.");
        }

        // Method to transfer money
        public void Transfer(long fromAccount, long toAccount, float amount)
        {
            if (!accounts.ContainsKey(fromAccount) || !accounts.ContainsKey(toAccount))
                throw new Exception("One or both accounts not found.");

            Withdraw(fromAccount, amount);
            Deposit(toAccount, amount);
        }

        // Method to get account and customer details
        public void GetAccountDetails(long accountNumber)
        {
            if (accounts.ContainsKey(accountNumber))
                accounts[accountNumber].PrintAccountInfo();
            else
                throw new Exception("Account not found.");
        }
    }
}
