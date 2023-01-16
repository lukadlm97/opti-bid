using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Persistence.Configurations
{
    public class ContractTypeConfiguration : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {

            builder.HasKey(contactType => contactType.ID);
            builder.Property(contactType => contactType.ID).ValueGeneratedOnAdd();
        //    builder.HasMany(contactType => contactType.Contacts).WithOne().HasForeignKey(contract => contract.ContactTypeID);

        }
    }
}
