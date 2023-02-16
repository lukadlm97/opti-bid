using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Persistence.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {

      
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(userToken => userToken.ID);
            builder.Property(userToken => userToken.ID).ValueGeneratedOnAdd();
        }
    }
}
