using Mc2.CrudTest.ModelFramework.Queries;
using System;

namespace Mc2.CrudTest.ApplicationService.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomerQueryFilter : IFilterObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BankAccount { get; set; }
    }
}