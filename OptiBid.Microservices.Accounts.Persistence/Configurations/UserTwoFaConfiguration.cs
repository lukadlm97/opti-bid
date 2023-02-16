using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OptiBid.Microservices.Accounts.Persistence.Configurations
{
    public class UserTwoFaConfiguration : IEntityTypeConfiguration<UserTwoFAAssets>
    {
        public void Configure(EntityTypeBuilder<UserTwoFAAssets> builder)
        {

            builder.HasKey(userTwoFa => userTwoFa.ID);
            builder.Property(userTwoFa => userTwoFa.ID).ValueGeneratedOnAdd();
        }
    }
}
