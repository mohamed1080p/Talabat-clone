
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



        // get all specifications

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications);

        // get by ID specifications
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);

    }
}
