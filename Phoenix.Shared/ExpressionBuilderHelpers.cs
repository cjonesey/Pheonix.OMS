using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Phoenix.Shared
{
	public static class ExpressionBuilderHelpers
	{
		/// <summary>
		/// Order by Query - You can only do this where the tables are joined using Includes (Otherwise it won't find the types)
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="source"></param>
		/// <param name="key"></param>
		/// <param name="desc"></param>
		/// <returns></returns>
		public static IOrderedQueryable<TEntity> OrderByTerm<TEntity>(this IQueryable<TEntity> source, string key, bool desc)
		{
			string command = desc ? "OrderByDescending" : "OrderBy";
			var type = typeof(TEntity);
			PropertyInfo? property = PredicateGenericHelper.GetPropertiesRecursively(type, key);
			ParameterExpression param = Expression.Parameter(type, "p");
			MemberExpression propertyAccess = PredicateGenericHelper.GetNestedExprProperty(param, key);
			var orderByExpression = Expression.Lambda(propertyAccess, param);
			var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
				source.Expression, Expression.Quote(orderByExpression));
			return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
		}
	}
}
