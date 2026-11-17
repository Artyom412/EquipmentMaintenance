using EquipmentMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Services
{
    public class CrudService(IDbContextFactory<AppDbContext> contextFactory) : ICrudService
    {
        public async Task AddRecordAsync<T>(T entity) where T : class
        {
            await using var context = await contextFactory.CreateDbContextAsync(); 

            if (entity is MaterialTransaction materialTransaction)
            {
                await ValidateMaterialTransactionAsync(context, materialTransaction);
            }

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

        public async Task RemoveRecordAsync<T>(T entity) where T : class
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExpenseOfMaterial>> GetExpenseOfMaterial()
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var report = await context.Statements
                .AsNoTracking()
                .GroupBy(m => m.Material.Name)
                .Select(g => new ExpenseOfMaterial
                {
                    MaterialName = g.Key,
                    Expense = g.Count()
                })
                .OrderByDescending(r => r.Expense)
                .ToListAsync();

            return report;
        }

        public async Task<IEnumerable<RecurringFaults>> GetRecurringFaults()
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var report = await context.Statements
                .AsNoTracking()
                .GroupBy(f => f.Name)
                .Select(g => new RecurringFaults
                {
                    FaultName = g.Key,
                    Quantity = g.Count()
                })
                .OrderByDescending(r => r.Quantity)
                .ToListAsync();

            return report;
        }

        public async Task<IEnumerable<AmountOfStatementsTable>> GetAmountOfStatementsReport()
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var report = await context.Statements
                .AsNoTracking()
                .GroupBy(s => s.Employee.Name)
                .Select(g => new AmountOfStatementsTable
                {
                    NameOfResponsiblePerson = g.Key,
                    AmountOfStatements = g.Count()
                })
                .OrderByDescending(r => r.AmountOfStatements)
                .ToListAsync();

            return report;
        }

        public async Task UpdateRecordAsync<T>(T entity) where T : class
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            if (entity is MaterialTransaction materialTransaction)
            {
                await ValidateMaterialTransactionAsync(context, materialTransaction);
            }

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        private static async Task ValidateMaterialTransactionAsync(
            AppDbContext context,
            MaterialTransaction transaction)
        {
            if (transaction.Quantity <= 0)
            {
                throw new InvalidOperationException("Количество материала должно быть больше нуля.");
            }

            if (transaction.TransactionType != TransactionType.Expense)
            {
                return;
            }

            var transactions = context.MaterialTransactions
                .Where(x => x.MaterialId == transaction.MaterialId && x.Id != transaction.Id);
            var income = await transactions
                .Where(x => x.TransactionType == TransactionType.Income)
                .Select(x => (long?)x.Quantity)
                .SumAsync() ?? 0;
            var expense = await transactions
                .Where(x => x.TransactionType == TransactionType.Expense)
                .Select(x => (long?)x.Quantity)
                .SumAsync() ?? 0;

            if (income - expense < transaction.Quantity)
            {
                throw new InvalidOperationException(
                    $"Недостаточно материала на складе. Доступно: {income - expense}, требуется: {transaction.Quantity}.");
            }
        }
    }
}
