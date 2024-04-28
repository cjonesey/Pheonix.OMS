using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
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
            MethodCallExpression call = Expression.Call(
                Expression.Property(param, key),
                method!,
                Expression.Constant(value, type));
            return Expression.Lambda<Func<T, bool>>(call, param);
        }
        public static Expression<Func<T, bool>> CreateExpressionCall<T, U>(
            string key,
            List<U> lookupValues,
            MethodInfo? method,
            Type type)
        {
            var param = Expression.Parameter(typeof(T));
            MethodCallExpression call = Expression.Call(
                Expression.Constant(lookupValues, typeof(List<U>)),
                method!,
                Expression.Property(param, key));
            return Expression.Lambda<Func<T, bool>>(call, param);
        }

        public static BaseValues.SearchType GetSearchTypeForObject(PropertyInfo? prop)
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

        public static Expression<Func<T, bool>> CreateExpressionCallFromList<T>(
            string key,
            string value,
            PropertyInfo? prop) where T : BaseEntity
        {
            if (prop.PropertyType == typeof(int))
            {
                var ids = PredicateGenericHelper.ConvertList<int>(value);
                return PredicateGenericHelper.CreateExpressionCall<T, int>(
                        key,
                        ids,
                        PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains),
                        prop.PropertyType);
            }
            if (prop.PropertyType == typeof(DateTime))
            {
                var ids = PredicateGenericHelper.ConvertList<DateTime>(value);
                return PredicateGenericHelper.CreateExpressionCall<T, DateTime>(
                        key,
                        ids,
                        PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains),
                        prop.PropertyType);

            }
            List<string> idString = value.Split('|').ToList();
            return PredicateGenericHelper.CreateExpressionCall<T, string>(
                    key,
                    idString,
                    PredicateGenericHelper.GetMethod(idString.GetType(), BaseValues.SearchType.Contains),
                    prop.PropertyType);
        }

        public static List<T> ConvertList<T>(string inputData)
        {
            List<T> list = new List<T>();
            inputData.Split('|').ToList().ForEach(x =>
            {
                if (CanChangeType(x, typeof(T)))
                {
                    try
                    {
                        list.Add((T)Convert.ChangeType(x, typeof(T)));
                    }
                    catch (Exception ex) { }
                }
            });
            return list;
        }
        public static bool CanChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
                return false;

            if (value == null)
                return false;

            IConvertible convertible = value as IConvertible;
            TypeCode typeCode = Type.GetTypeCode(convertible.GetType());
            if (convertible == null)
                return false;

            return true;
        }

    }
}
