namespace Core.Entities.OrderAgregates
{
    public class OrderAddress
    {
        public OrderAddress()
        {
            
        }
        public OrderAddress(string country, string street, string city, string state, string? zipCode)
        {
            Country = country;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public required string Country { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public string? ZipCode { get; set; }
    }
}
