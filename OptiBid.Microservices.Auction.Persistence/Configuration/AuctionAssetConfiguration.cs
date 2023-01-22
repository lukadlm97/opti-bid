using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Auction.Domain.Entities;

namespace OptiBid.Microservices.Auction.Persistence.Configuration
{
    public class AuctionAssetConfiguration : IEntityTypeConfiguration<AuctionAsset>
    {
        public void Configure(EntityTypeBuilder<AuctionAsset> builder)
        {
            builder.ToTable("AuctionAssets");
            builder.Property(customer => customer.StartDate).ValueGeneratedOnAdd();
            builder.HasDiscriminator<string>("AssetType")
                .HasValue<Product>("Product")
                .HasValue<Service>("Service");
        }
    }
}
