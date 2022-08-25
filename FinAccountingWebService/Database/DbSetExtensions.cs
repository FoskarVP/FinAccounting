using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace FinAccountingWebService.Database
{
    public static class DbSetExtensions
    {
        public static void AddIfNotExists<T>(this DbSet<T> dbSet, T entity) 
            where T : class, new()
        {
            var exists = dbSet.Any(el => entity.Equals(el));

            if (!exists)
                dbSet.Add(entity);
        }
    }
}
