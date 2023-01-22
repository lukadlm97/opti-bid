using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Auction.Domain.Entities;

namespace OptiBid.Microservices.Auction.Persistence.Configuration
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.Property(bid => bid.BidDate).ValueGeneratedOnAdd();
        }
    }
}
