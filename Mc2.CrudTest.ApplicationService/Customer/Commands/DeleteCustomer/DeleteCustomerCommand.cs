using Mc2.CrudTest.ModelFramework.Command;
using System;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : ICommand
    {
        public int CustomerId { get; set; }
        
    }

}