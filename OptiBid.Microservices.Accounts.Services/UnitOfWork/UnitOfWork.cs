using OptiBid.Microservices.Accounts.Data.Repository;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Persistence;

namespace OptiBid.Microservices.Accounts.Services.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork,IDisposable
    {
        private readonly AccountsContext _accountsContext;

        public UnitOfWork(AccountsContext accountsContext)
        {
            _accountsContext = accountsContext;
            _usersRepository = new AccountRepository(_accountsContext);
            _skillRepository = new Repository<Skill>(_accountsContext);
            _contactRepository=new Repository<Contact>(_accountsContext);
            _professionRepository=new Repository<Profession>(_accountsContext);
            _contactTypeRepository=new Repository<ContactType>(_accountsContext);
            _countryRepository=new Repository<Country>(_accountsContext);
            _userRolesRepository = new Repository<UserRole>(_accountsContext);
        }
        public IAccountRepository _usersRepository { get; set; }
        public IRepository<Skill> _skillRepository { get; set; }
        public IRepository<Contact> _contactRepository { get; set; }
        public IRepository<ContactType> _contactTypeRepository { get; set; }
        public IRepository<Profession> _professionRepository { get; set; }
        public IRepository<Country> _countryRepository { get; set; }
        public IRepository<UserRole> _userRolesRepository { get; set; }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _accountsContext.SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _accountsContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
