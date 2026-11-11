using EquipmentMaintenance.Models;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Services;

public interface IStatementService
{
    Task SaveAsync(Statement statement);
    Task DeleteAsync(Statement statement);
}
