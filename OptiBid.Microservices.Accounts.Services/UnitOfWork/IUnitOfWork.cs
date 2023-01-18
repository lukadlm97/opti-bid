using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Data.Repository;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAccountRepository _usersRepository { get; set; }
        IRepository<Skill> _skillRepository { get; set; }
        IRepository<Contact> _contactRepository { get; set; }
        IRepository<ContactType> _contactTypeRepository { get; set; }
        IRepository<Profession> _professionRepository { get; set; }
        IRepository<Country> _countryRepository { get; set; }
        IRepository<UserRole> _userRolesRepository { get; set; }

        Task Commit(CancellationToken cancellationToken=default);
    }
}
