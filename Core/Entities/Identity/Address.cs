using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public required string Country { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public string? ZipCode { get; set; }
        [ForeignKey("User")]
        public string UserId {  get; set; }
        public ApplicationUser User { get; set; }
    }
}