using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Services
{
    public interface ICrudService
    {
        Task AddRecordAsync<T>(T entity) where T : class;
        Task<IEnumerable<T>> GetRecordsAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task RemoveRecordAsync<T>(T entity) where T : class;
        Task UpdateRecordAsync<T>(T entity) where T : class;
        Task<IEnumerable<AmountOfStatementsTable>> GetAmountOfStatementsReport();
        Task<IEnumerable<ExpenseOfMaterial>> GetExpenseOfMaterial();
        Task<IEnumerable<RecurringFaults>> GetRecurringFaults();
    }
}
