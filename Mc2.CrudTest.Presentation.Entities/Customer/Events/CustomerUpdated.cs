using Mc2.CrudTest.ModelFramework.Events;
using System;

namespace Mc2.CrudTest.DomainModel.Customer.Events
{
    public class CustomerUpdated : IEvent
    {
        public int CustomerId { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public CustomerUpdated(int customerId,string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }
    }
}
