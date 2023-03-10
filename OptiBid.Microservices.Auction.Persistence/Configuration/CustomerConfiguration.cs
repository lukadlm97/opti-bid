using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Auction.Domain.Entities;

namespace OptiBid.Microservices.Auction.Persistence.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(customer => customer.DateOpened).ValueGeneratedOnAdd();

        }
    }
}
