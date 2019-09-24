using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsManagementSystem
{
    public class Customer : IPoco, IUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditCardNumber { get; set; }

        public Customer()
        {
        }

        public Customer(string firstName, string lastName, string userName, string password, string address, string phoneNo, string creditCardNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Address = address;
            PhoneNo = phoneNo;
            CreditCardNumber = creditCardNumber;
        }
        public static bool operator ==(Customer a, Customer b)
        {
            if (a.Id == b.Id)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Customer a, Customer b)
        {
            return !(a.Id == b.Id);
        }

        public override string ToString()
        {
            return $"FirstName: {FirstName} LastName: {LastName} UserName: {UserName} Password: {Password}" +
                $" Address: {Address} PhoneNo: {PhoneNo} CreditCardNumber: {CreditCardNumber}";
        }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }
    }
}
