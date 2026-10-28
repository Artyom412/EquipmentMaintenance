using EquipmentMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Services
{
    public class CrudService(AppDbContext context) : ICrudService
    {
        private readonly AppDbContext _context = context;

        public async Task AddRecordAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetRecordsAsync<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            var collection = _context.Set<T>()
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
