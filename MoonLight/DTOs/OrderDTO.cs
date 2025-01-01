namespace MoonLight.API.DTOs
{
    public class OrderDTO
    {
        public required string BasketId { get; set; }
        public required int DeliveryMethodId { get; set; }
        public required AddressDTO ShipToAddress { get; set; }

    }
}
