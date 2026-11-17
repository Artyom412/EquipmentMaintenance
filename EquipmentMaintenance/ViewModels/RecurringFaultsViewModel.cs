using Avalonia.Collections;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class RecurringFaultsViewModel : ViewModelBase
    {
        private readonly ICrudService _crudService;

        public AvaloniaList<RecurringFaults> RecurringFaults { get; } = [];

        public RecurringFaultsViewModel(ICrudService crudService)
        {
            _crudService = crudService;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var table = await _crudService.GetRecurringFaults();
            RecurringFaults.AddRange(table);
        }
    }
}
