using Mc2.CrudTest.ModelFramework.Entities;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;

namespace Mc2.CrudTest.DomainModel.Customer.Entities
{
    public class Customer : BaseAggregateRoot<int>
    {
        public FirstName FirstName { get; private set; }


        private Customer()
        {
        }
        protected override void ValidateInvariants()
        {

        }
    }
}