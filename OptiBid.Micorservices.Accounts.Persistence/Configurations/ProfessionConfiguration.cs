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
    internal class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {

            builder.HasKey(profession => profession.ID);
            builder.Property(profession => profession.ID).ValueGeneratedOnAdd();
           // builder.HasMany(profession => profession.Skills).WithOne().HasForeignKey(skill => skill.ProfessionID);

        }
    }
}
