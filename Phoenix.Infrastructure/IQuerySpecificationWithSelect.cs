using System.Linq.Expressions;

namespace Phoenix.Infrastructure
{
    public interface IQuerySpecificationWithSelect<TEntity, TOutput> : IQuerySpecification<TEntity>
        where TEntity : class
        where TOutput : class
    {
        Expression<Func<TEntity, TOutput>> SelectClause { get; }
        void Select(Expression<Func<TEntity, TOutput>>? select);
    }
}
