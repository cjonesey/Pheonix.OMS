using System.Linq.Expressions;

namespace Phoenix.Infrastructure
{
    public class QuerySpecificationWithSelect<TEntity, TOutput>
        : QuerySpecification<TEntity>,
        IQuerySpecificationWithSelect<TEntity, TOutput> where TEntity : class
        where TOutput : class
    {

        public Expression<Func<TEntity, TOutput>>? SelectClause { get; private set; }

        public void Select(Expression<Func<TEntity, TOutput>>? select)
        {
            SelectClause = select;
        }

    }
}
