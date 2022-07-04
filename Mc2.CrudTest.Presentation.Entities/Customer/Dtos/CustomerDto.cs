namespace Mc2.CrudTest.DomainModel.Customer.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public string Title { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }
        public long ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }
}
