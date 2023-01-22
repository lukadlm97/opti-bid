
namespace OptiBid.Microservices.Auction.Domain.DTOs
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string DateOpened { get; set; }
    }
}
