using Mc2.CrudTest.ModelFramework.Command;
using System;
using System.Collections.Generic;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : ICommand
    {
        public string FirstName { get; set; }
         }
    
}