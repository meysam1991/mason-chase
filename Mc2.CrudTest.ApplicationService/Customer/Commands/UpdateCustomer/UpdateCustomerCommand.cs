using Mc2.CrudTest.ModelFramework.Command;
using System;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : ICommand
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

}