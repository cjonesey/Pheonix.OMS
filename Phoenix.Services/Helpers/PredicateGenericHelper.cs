using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace Phoenix.Services.Helpers
{
    public static class PredicateGenericHelper
    {
        public static MethodInfo? GetMethod(Type type, BaseValues.SearchType searchType)
        {
            string methodName = Enum.GetName(typeof(BaseValues.SearchType), searchType)!;
            return type.GetMethods().Where(x => x.Name == methodName).FirstOrDefault();
        }

        public static Expression<Func<T, bool>> CreateExpressionCall<T>(
            string key,
            string value,
            MethodInfo? method,
            Type type)
        {
            var param = Expression.Parameter(typeof(T));
            MethodCallExpression call = Expression.Call(Expression.Property(param, key),
                method!,
                Expression.Constant(value, type));
            return Expression.Lambda<Func<T, bool>>(call, param);
        }

        public static BaseValues.SearchType GetIDForPassedInObject(PropertyInfo? prop)
        {
            BaseValues.SearchType searchType = BaseValues.SearchType.Equals;
            if (prop == null)
                return searchType;

                var attributes = prop.GetCustomAttributes(typeof(Searchable), false);
            if (attributes.Length > 0)
            {
                foreach(var att in attributes)
                {
                    if (att is Searchable)
                    {
                         searchType = ((Searchable)att).SearchType;
                    }
                }
            }
            return searchType;
        }
    }
}
