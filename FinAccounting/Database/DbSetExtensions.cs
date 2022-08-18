using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace FinAccounting.Database
{
    public static class DbSetExtensions
    {
        public static void AddIfNotExists<T>(this DbSet<T> dbSet, T entity) 
            where T : class, new()
        {
            var exists = dbSet.Any(el => el.Equals(entity));

            if (!exists)
                dbSet.Add(entity);
        }
    }
}
