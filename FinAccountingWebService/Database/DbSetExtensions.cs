using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;

namespace FinAccountingWebService.Database
{
    public static class DbSetExtensions
    {
        public static void AddIfNotExists<T>(this DbSet<T> dbSet, ref T entity, System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
            where T : class, new()
        {
            var existsElement = predicate != null ? dbSet.FirstOrDefault(predicate) : null;

            if (existsElement != null)
            {
                entity = existsElement;
            }
            else
            {
                dbSet.Add(entity);
            }
        }
    }
}
