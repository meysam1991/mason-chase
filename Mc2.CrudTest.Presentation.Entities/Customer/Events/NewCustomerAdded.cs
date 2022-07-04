using Mc2.CrudTest.ModelFramework.Events;

namespace Mc2.CrudTest.DomainModel.Customer.Events
{
    public class NewCustomerAdded : IEvent
    {
        public string UserId { get; }
        public long CityId { get; }
        public string Title { get; }
        public string AddressLine { get; }
        public string PostalCode { get; }

        public NewCustomerAdded(string userId, long cityId, string title, string addressLine, string postalCode)
        {
            UserId = userId;
            CityId = cityId;
            Title = title;
            AddressLine = addressLine;
            PostalCode = postalCode;
        }
    }
}
