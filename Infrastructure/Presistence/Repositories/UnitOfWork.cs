
using Domain.Contracts;
using Domain.Models;
using persistence.Data;

namespace persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = new();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if(_repositories.ContainsKey(typeof(TEntity).Name))
            {
                return (IGenericRepository<TEntity, TKey>)_repositories[typeof(TEntity).Name];
            }
            else
            {
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories.Add(typeof(TEntity).Name, repo);
                return repo;
            }
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
