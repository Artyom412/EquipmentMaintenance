using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Services
{
    public interface ICrudService
    {
        Task AddRecordAsync<T>(T entity) where T : class;
        Task<IEnumerable<T>> GetRecordsAsync<T>() where T : class;
    }
}
