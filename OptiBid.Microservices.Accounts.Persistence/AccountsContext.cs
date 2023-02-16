using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Persistence
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserTwoFAAssets> UserTwoFaAssets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsContext).Assembly);
        }
    }
}
