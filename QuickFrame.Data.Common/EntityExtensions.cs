using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuickFrame.Data.Common
{
    public static class EntityExtensions
    {
		public static IQueryable<TSource> IsDeleted<TSource>(this IQueryable<TSource> source, bool val) {
			var parameterExpression = Expression.Parameter(typeof(TSource));
			var propertyExpression = Expression.Property(parameterExpression, "IsDeleted");
			var boolExpression = Expression.Equal(propertyExpression, Expression.Constant(val));
			var lambdaExpression = Expression.Lambda<Func<TSource, bool>>(boolExpression, parameterExpression);
			var compiled = lambdaExpression.Compile();
			return source.Where(lambdaExpression);
		}
		/// <summary>
		/// Orders a query by the specified property.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <param name="source">The query to order.</param>
		/// <param name="propertyName">Name of the property to use for ordering.</param>
		/// <returns>An IQueryable representing the original query with the OrderBy clause appended.</returns>
		public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName) {
			var parameter = Expression.Parameter(typeof(TSource), "obj");
			var member = (Expression)parameter;
			if(propertyName.Contains(".")) {
				foreach(var obj in propertyName.Split('.'))
					member = Expression.PropertyOrField(member, obj);
			} else {
				member = Expression.PropertyOrField(parameter, propertyName);
			}
			var lambda = Expression.Lambda(member, parameter);
			Type[] argTypes = { source.ElementType, lambda.Body.Type };
			var methodCall = Expression.Call(typeof(Queryable), "OrderBy", argTypes, source.Expression, lambda);
			return source.Provider.CreateQuery<TSource>(methodCall);
		}

		/// <summary>
		/// Orders a query by the specified property.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <param name="source">The query to order.</param>
		/// <param name="propertyName">Name of the property to use for ordering.</param>
		/// <returns>An IQueryable representing the original query with the OrderBy clause appended.</returns>
		public static IQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string propertyName) {
			var parameter = Expression.Parameter(typeof(TSource), "obj");
			var member = (Expression)parameter;
			if(propertyName.Contains(".")) {
				foreach(var obj in propertyName.Split('.'))
					member = Expression.PropertyOrField(member, obj);
			} else {
				member = Expression.PropertyOrField(parameter, propertyName);
			}
			var lambda = Expression.Lambda(member, parameter);
			Type[] argTypes = { source.ElementType, lambda.Body.Type };
			var methodCall = Expression.Call(typeof(Queryable), "OrderByDescending", argTypes, source.Expression, lambda);
			return source.Provider.CreateQuery<TSource>(methodCall);
		}
	}
}
