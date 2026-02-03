using Domain.Contracts;
using Domain.Models;
using System.Linq.Expressions;

namespace Service.Specifications
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
    }
}
