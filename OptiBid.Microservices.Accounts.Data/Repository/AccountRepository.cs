using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Persistence;

namespace OptiBid.Microservices.Accounts.Data.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly AccountsContext accountsContext;

        public AccountRepository(AccountsContext accountsContext)
        {
            this.accountsContext = accountsContext;
        }
        public async Task<User> RegisterUser(User user,CancellationToken cancellationToken=default)
        {
            await this.accountsContext.Users.AddAsync(user, cancellationToken);
            return user;
        }

        public void UpdateUser(User user)
        {
            this.accountsContext.Users.Update(user);
            
        }

        public void DeleteUser(User user)
        {
            this.accountsContext.Users.Remove(user);
        }

        public async Task<User?> GetById(int id,CancellationToken cancellationToken=default)
        {
            return await this.accountsContext.Users.Include(x=>x.UserRole)
                .Include(x=>x.Country)
                .Include(x=>x.Contacts)
                .Include(x=>x.Skills)
                .FirstOrDefaultAsync(x => x.ID == id, cancellationToken);
        }

        public IEnumerable<User> GetAll()
        {
            return  this.accountsContext.Users.Include(x=>x.UserRole).AsEnumerable();
        }

        public async Task<User?> GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            return await this.accountsContext.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        }
    }
}
