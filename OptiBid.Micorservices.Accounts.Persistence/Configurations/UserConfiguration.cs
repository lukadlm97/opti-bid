using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Entities;
namespace OptiBid.Microservices.Accounts.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.HasKey(user => user.ID);
            builder.Property(user => user.ID).ValueGeneratedOnAdd();
            builder.Property(user => user.FirstName).HasMaxLength(250);
            builder.Property(user => user.LastName).HasMaxLength(250);
          //  builder.HasOne(user => user.UserRole);
          //  builder.HasMany(user => user.Skills).WithOne().HasForeignKey(skill => skill.UserID);
         //   builder.HasMany(user => user.Contacts).WithOne().HasForeignKey(contact => contact.UserID);

        }
    }
}
