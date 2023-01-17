using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;

namespace OptiBid.Microservices.Accounts.Persistence.Configurations
{
    
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

           builder.HasKey(country => country.ID);
           builder.Property(country => country.ID).ValueGeneratedNever();
          //  builder.HasMany(country => country.Users).WithOne().HasForeignKey(user => user.CountryID);

        }
    }
}
