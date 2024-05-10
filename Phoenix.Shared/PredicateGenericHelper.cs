using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Phoenix.Shared
{
	public static class PredicateGenericHelper
    {
        /// <summary>
        /// Gets the method type for type - note could be null if you try and return an invalid combination 
        /// e.g. Date and Contains
        /// </summary>
        /// <param name="type"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public static MethodInfo? GetMethod(Type type, BaseValues.SearchType searchType, bool isEnumerable = false)
        {
			string methodName = Enum.GetName(typeof(BaseValues.SearchType), searchType)!;
            if (isEnumerable)
            {
                return type.GetMethods().Where(x => x.Name == methodName).FirstOrDefault();
            }
            Type uType = Nullable.GetUnderlyingType(type)!;
			return type.GetMethod(methodName, new[] { uType == null ? type : uType });
        }


        /// <summary>
        /// Creates an expression call for Nullable and Non Nullable fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="method"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateExpressionCall<T>(
            string key,
            string value,
            MethodInfo? method,
            Type type)
        {
			Type uType = Nullable.GetUnderlyingType(type)!;
			var param = Expression.Parameter(typeof(T), "x");
			var member = Expression.Property(param, key);
            Expression typeFilter = default;
            if (uType != null)
            {
                var filter = Expression.Constant(
                    Convert.ChangeType(value, type.GetGenericArguments()[0]));
                typeFilter = Expression.Convert(filter, member.Type);

                var call = Expression.Equal(
                    member,
                    typeFilter);
                return Expression.Lambda<Func<T, bool>>(call, param);
            }
            else
            {
                typeFilter = Expression.Constant(Convert.ChangeType(value, type), type);
                var call = Expression.GreaterThan(
                    member,
                    typeFilter);
                return Expression.Lambda<Func<T, bool>>(call, param);
            }
            //         if (uType != null)
            //         {
            //             var filter = Expression.Constant(
            //                 Convert.ChangeType(value, type.GetGenericArguments()[0]));
            //             Expression typeFilter = Expression.Convert(filter, member.Type);

            //             var call = Expression.Equal(
            //                 member, 
            //                 typeFilter);
            //             return Expression.Lambda<Func<T, bool>>(call, param) ;
            //}
            //else
            //         {
            //	MethodCallExpression call = Expression.Equal(
            //		member,
            //		MethodInfo.gr,
            //		Expression.Constant(Convert.ChangeType(value, type), type));
            //	return Expression.Lambda<Func<T, bool>>(call, param);
            //}
        }

        public static Expression<Func<T, bool>> CreateExpressionDynamicCall<T>(
            string key,
            string value,
            BaseValues.SearchType searchType,
            Type type)
        {
            Type uType = Nullable.GetUnderlyingType(type)!;
            var param = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(param, key);
            Expression typeFilter = default;
            Expression call = default;
            if (uType != null)
            {
                var filter = Expression.Constant(
                    Convert.ChangeType(value, type.GetGenericArguments()[0]));
                typeFilter = Expression.Convert(filter, member.Type);
            }
            else
            {
                typeFilter = Expression.Constant(Convert.ChangeType(value, type), type);
            }

            switch (searchType)
            {
                case BaseValues.SearchType.LessThan:
                    call = Expression.LessThan(
                        member,
                        typeFilter);
                    break;
                case BaseValues.SearchType.LessThanOrEqual:
                    call = Expression.LessThanOrEqual(
                        member,
                        typeFilter);
                    break;
                case BaseValues.SearchType.GreaterThan:
                    call = Expression.GreaterThan(
                        member,
                        typeFilter);
                    break;
                case BaseValues.SearchType.GreaterThanOrEqual:
                    call = Expression.GreaterThanOrEqual(
                        member,
                        typeFilter);
                    break;
                case BaseValues.SearchType.Contains:
                case BaseValues.SearchType.StartsWith:
                case BaseValues.SearchType.EndsWith:
                    call = Expression.Call(
                        member,
                        GetMethod(type, searchType, false),
                        typeFilter);
                    break;
                default:
                    call = Expression.Equal(
                        member,
                        typeFilter);
                    break;
            }
            return Expression.Lambda<Func<T, bool>>(call, param);
        }
        /// <summary>
        /// Create expression from list - not pretty, however it works
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateExpressionCallFromList<T>(
            string key,
            string value,
            Type type) where T : class
        {
            if (type == typeof(int))
            {
                var ids = PredicateGenericHelper.ConvertList<int>(value);
                return PredicateGenericHelper.CreateExpressionCall<T, int>(
                        key,
                        ids,
                        PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains, true),
                        type);
            }
            else if (type == typeof(Nullable<int>))
            {
                var ids = PredicateGenericHelper.ConvertList<int?>(value);
                return PredicateGenericHelper.CreateExpressionCall<T, int?>(
                        key,
                        ids,
                        PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains, true),
                        type);
            }
            else if (type == typeof(DateTime))
            {
                var ids = PredicateGenericHelper.ConvertList<DateTime>(value);
                return PredicateGenericHelper.CreateExpressionCall<T, DateTime>(
                        key,
                        ids,
                        PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains, true),
                        type);

            }
			else if (type == typeof(Nullable<DateTime>))
			{
				var ids = PredicateGenericHelper.ConvertList<DateTime?>(value);
				return PredicateGenericHelper.CreateExpressionCall<T, DateTime?>(
						key,
						ids,
						PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains, true),
						type);

			}
			if (type == typeof(Decimal))
			{
				var ids = PredicateGenericHelper.ConvertList<Decimal>(value);
				return PredicateGenericHelper.CreateExpressionCall<T, Decimal>(
						key,
						ids,
						PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains, true),
						type);

			}
			else if (type == typeof(Nullable<Decimal>))
			{
				var ids = PredicateGenericHelper.ConvertList<Decimal?>(value);
				return PredicateGenericHelper.CreateExpressionCall<T, Decimal?>(
						key,
						ids,
						PredicateGenericHelper.GetMethod(ids.GetType(), BaseValues.SearchType.Contains, true),
						type);

			}
			List<string> idString = value.Split('|').ToList();
			return PredicateGenericHelper.CreateExpressionCall<T, string>(
                    key,
                    idString,
					PredicateGenericHelper.GetMethod(idString.GetType(), BaseValues.SearchType.Contains, true),
                    type);
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


		/// <summary>
		/// Convert List of types - useful for doing contains
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static List<T> ConvertList<T>(string inputData)
        {
            List<T> list = new List<T>();
            inputData.Split('|').ToList().ForEach(x =>
            {
                if (CanChangeType(x, typeof(T)))
                {
                    try
                    {
                        var uType = Nullable.GetUnderlyingType(typeof(T));
                        list.Add((T)Convert.ChangeType(x, uType == null ? typeof(T) : uType));
                    }
                    catch (Exception ex) { }
                }
            });
            return list;
        }

        /// <summary>
        /// Can Change Type - Does create errors (ToDo)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
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
