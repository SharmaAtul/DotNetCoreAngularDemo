using DotNetCoreAngularDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IQueryObject queryObj,  Dictionary<string, Expression<Func<T, object>>> sortColumnMap)
        {
            if (string.IsNullOrWhiteSpace(queryObj.SortColumn) || !sortColumnMap.ContainsKey(queryObj.SortColumn))
                return query;
            
            if (queryObj.IsSortAscending)
                return query.OrderBy(sortColumnMap[queryObj.SortColumn]);
            else
                return query.OrderByDescending(sortColumnMap[queryObj.SortColumn]);
            
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObject)
        {
            if (queryObject.PageSize <= 0)
                queryObject.PageSize = 10;

            if (queryObject.PageIndex <= 0)
                queryObject.PageIndex = 1;

            return query.Skip((queryObject.PageIndex - 1) * queryObject.PageSize).Take(queryObject.PageSize);
        }
    }
}
