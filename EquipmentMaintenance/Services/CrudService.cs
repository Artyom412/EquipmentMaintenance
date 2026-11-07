using EquipmentMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Services
{
    public class CrudService(IDbContextFactory<AppDbContext> contextFactory) : ICrudService
    {
        public async Task AddRecordAsync<T>(T entity) where T : class
        {
            await using var context = await contextFactory.CreateDbContextAsync(); 

            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetRecordsAsync<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var collection = context.Set<T>()
                .AsNoTracking();

            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    collection = collection.Include(item);
                }
            }

            return await collection.ToListAsync();
        }
    }
}
