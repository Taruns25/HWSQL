using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace HMSbank.dao
{
    public interface IAccountService
    {
        void CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string address);
        void CreateAccount(int accountNumber, string accountType, decimal initialBalance, int customerId);
        void Deposit(int accountNumber, decimal amount);
        void Withdraw(int accountNumber, decimal amount);
        void Transfer(int fromAccountNumber, int toAccountNumber, decimal amount);
        void DisplayAccountDetails(int accountNumber);
        void DisplayCustomerDetails(int customerId);
    }
}

