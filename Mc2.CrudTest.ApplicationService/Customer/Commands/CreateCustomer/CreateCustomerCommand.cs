using Mc2.CrudTest.ModelFramework.Command;
using System;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public CreateCustomerCommand()
        {
            DateOfBirth = DateTime.Now.ToString();
        }
    }

}