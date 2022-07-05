using Mc2.CrudTest.DomainModel.Customer.Events;
using Mc2.CrudTest.ModelFramework.Entities;
using Mc2.CrudTest.Shared.ValueObjects.BankAccountNumber;
using Mc2.CrudTest.Shared.ValueObjects.Email;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;
using Mc2.CrudTest.Shared.ValueObjects.LastName;
using Mc2.CrudTest.Shared.ValueObjects.PhoneNumber;
using System;
using Mc2.CrudTest.Shared.ValidatorExtensions;
using Mc2.CrudTest.Shared.StringUtils;
using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.DomainModel.Customer.Exception;

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

        private Customer()
        {
        }
        protected override void ValidateInvariants()
        {
            

            if (DateOfBirth==null)
                throw new InvalidEmailException(new InputParameter
                { PropertyName = nameof(Email), AttemptedValue = Email.Value });
        }
    }
}