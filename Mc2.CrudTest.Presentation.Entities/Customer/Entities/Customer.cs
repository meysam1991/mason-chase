using Mc2.CrudTest.ModelFramework.Entities;
using Mc2.CrudTest.Shared.ValueObjects.BankAccountNumber;
using Mc2.CrudTest.Shared.ValueObjects.Email;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;
using Mc2.CrudTest.Shared.ValueObjects.LastName;
using Mc2.CrudTest.Shared.ValueObjects.PhoneNumber;
using System;

namespace Mc2.CrudTest.DomainModel.Customer.Entities
{
    public class Customer : BaseAggregateRoot<int>
    {
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public BankAccountNumber BankAccountNumber { get; private set; }

        public Customer(FirstName firstName, LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber, Email email, BankAccountNumber bankAccountNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }

        private Customer()
        {
        }
        protected override void ValidateInvariants()
        {

        }
    }
}