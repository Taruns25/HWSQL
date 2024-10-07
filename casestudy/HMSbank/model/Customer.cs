using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSbank.model
{
    public class Customer
    {
        // Confidential attributes
        private int customerId;
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
        private string address;

        // Default constructor
        public Customer() { }

        // Overloaded constructor
        public Customer(int customerId, string firstName, string lastName, string email, string phoneNumber, string address)
        {
            this.customerId = customerId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }

        // Getters and Setters
        public int CustomerId { get => customerId; set => customerId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Address { get => address; set => address = value; }

        // Method to print customer info
        public void PrintCustomerInfo()
        {
            Console.WriteLine($"Customer ID: {customerId}");
            Console.WriteLine($"Name: {firstName} {lastName}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone: {phoneNumber}");
            Console.WriteLine($"Address: {address}");
        }
    }
}

