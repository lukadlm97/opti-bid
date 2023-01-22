

namespace OptiBid.Microservices.Auction.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);
    }
}
