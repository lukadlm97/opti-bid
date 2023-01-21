using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Data.Repository
{
    public interface IAccountRepository
    {
        Task<User> RegisterUser(User user,CancellationToken cancellationToken=default);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<User?> GetById(int id, CancellationToken cancellationToken = default);
        IEnumerable<User> GetAll();
        Task<User?> GetByUsername(string username,CancellationToken cancellationToken=default);
    }
}
