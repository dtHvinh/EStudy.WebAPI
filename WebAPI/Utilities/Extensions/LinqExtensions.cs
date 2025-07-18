
using System.Linq.Expressions;
using System.Reflection;

namespace WebAPI.Utilities.Extensions;

public static class LinqExtensions
{
    public static IOrderedQueryable<TSource> OrderBy<TSource>(
           this IQueryable<TSource> query, string propertyName)
    {
        var entityType = typeof(TSource);

        //Create x=>x.PropName
        var propertyInfo = entityType.GetProperty(propertyName);
        ParameterExpression arg = Expression.Parameter(entityType, "x");
        MemberExpression property = Expression.Property(arg, propertyName);
        var selector = Expression.Lambda(property, [arg]);

        //Get System.Linq.Queryable.OrderBy() method.
        var enumarableType = typeof(Queryable);
        var method = enumarableType.GetMethods()
             .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
             .Where(m =>
             {
                 var parameters = m.GetParameters().ToList();
                 //Put more restriction here to ensure selecting the right overload                
                 return parameters.Count == 2;//overload that has 2 parameters
             }).Single();
#nullable disable
        //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        MethodInfo genericMethod = method
             .MakeGenericMethod(entityType, propertyInfo.PropertyType);

        /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
          Note that we pass the selector as Expression to the method and we don't compile it.
          By doing so EF can extract "order by" columns and generate SQL for it.*/
        var newQuery = (IOrderedQueryable<TSource>)genericMethod
             .Invoke(genericMethod, [query, selector]);
        return newQuery;
#nullable enable
    }

    public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
       this IQueryable<TSource> query, string propertyName)
    {
        var entityType = typeof(TSource);

        //Create x=>x.PropName
        var propertyInfo = entityType.GetProperty(propertyName);
        ParameterExpression arg = Expression.Parameter(entityType, "x");
        MemberExpression property = Expression.Property(arg, propertyName);
        var selector = Expression.Lambda(property, [arg]);

        //Get System.Linq.Queryable.OrderBy() method.
        var enumarableType = typeof(Queryable);
        var method = enumarableType.GetMethods()
             .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition)
             .Where(m =>
             {
                 var parameters = m.GetParameters().ToList();
                 //Put more restriction here to ensure selecting the right overload                
                 return parameters.Count == 2;//overload that has 2 parameters
             }).Single();
#nullable disable
        //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        MethodInfo genericMethod = method
             .MakeGenericMethod(entityType, propertyInfo.PropertyType);

        /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
          Note that we pass the selector as Expression to the method and we don't compile it.
          By doing so EF can extract "order by" columns and generate SQL for it.*/
        var newQuery = (IOrderedQueryable<TSource>)genericMethod
             .Invoke(genericMethod, [query, selector]);
        return newQuery;
#nullable enable
    }

    public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> query, int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
            throw new ArgumentOutOfRangeException("Page number and size must be greater than zero.");
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
