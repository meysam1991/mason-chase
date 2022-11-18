using Mc2.CrudTest.DomainModel.Customer.Events;
using Mc2.CrudTest.ModelFramework.Entities;
using Mc2.CrudTest.Shared.ValueObjects.BankAccountNumber;
using Mc2.CrudTest.Shared.ValueObjects.Email;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;
using Mc2.CrudTest.Shared.ValueObjects.LastName;
using Mc2.CrudTest.Shared.ValueObjects.PhoneNumber;
using System;
using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.DomainModel.Customer.Exception;
 

namespace Mc2.CrudTest.DomainModel.Customer.Entities
{
    public class Customer : BaseAggregateRoot<int>, ISoftDeleteEntity
    {
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public BankAccountNumber BankAccountNumber { get; private set; }

        public Customer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            FirstName =new FirstName( firstName);
            LastName =new LastName( lastName);
            DateOfBirth = dateOfBirth;
            PhoneNumber =new PhoneNumber( phoneNumber);
            Email =new Email( email);
            BankAccountNumber =new BankAccountNumber( bankAccountNumber);
            ValidateInvariants();
            AddEvent(
               new NewCustomerAdded(firstName,lastName,dateOfBirth,phoneNumber,email,bankAccountNumber));
        }

        public void Update(string firstName, string lastName, DateTime dateOfBirth, string email, string bankAccountNumber, string phoneNumber)
        {
            LastName = new LastName(lastName);
            DateOfBirth = dateOfBirth;
            PhoneNumber = new PhoneNumber(phoneNumber);
            Email = new Email(email);
            BankAccountNumber = new BankAccountNumber(bankAccountNumber);
            ValidateInvariants();
            AddEvent(
               new CustomerUpdated(Id,firstName,lastName,dateOfBirth,phoneNumber,email,bankAccountNumber));
        }

        private Customer()
        {
        }

        private bool ValidateTime(string dateInString)
        {
            DateTime temp;
            if (DateTime.TryParse(dateInString, out temp))
            {
                return true;
            }
            return false;
        }

        protected override void ValidateInvariants()
        {
            if (!ValidateTime(DateOfBirth.ToString()))
                throw new InvalidDateOfBirthException(new InputParameter
                { PropertyName = nameof(DateOfBirth), AttemptedValue = DateOfBirth.ToString() });
        }
    }
}