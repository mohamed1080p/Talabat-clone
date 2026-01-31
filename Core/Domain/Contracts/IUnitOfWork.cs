
using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        // get IGenericRepository<TEntity,Tkey>

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;

        // Save changes to database and return the number of affected records
        Task<int> SaveChanges();
    }
}
