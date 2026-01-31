
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        // get all

        Task<IEnumerable<TEntity>> GetAllAsync();

        // get by ID
        Task<TEntity?> GetByIdAsync(TKey id);

        //add
        Task AddAsync(TEntity entity);


        //update
        void Update(TEntity entity);

        //remove
        void Remove(TEntity entity);
        
    }
}
