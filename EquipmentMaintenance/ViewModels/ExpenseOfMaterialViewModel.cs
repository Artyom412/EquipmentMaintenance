using Avalonia.Collections;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class ExpenseOfMaterialViewModel : ViewModelBase
    {
        private readonly ICrudService _crudService;

        public AvaloniaList<ExpenseOfMaterial> ExpenseOfMaterials { get; } = [];

        public ExpenseOfMaterialViewModel(ICrudService crudService)
        {
            _crudService = crudService;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var items = await _crudService.GetExpenseOfMaterial();
            ExpenseOfMaterials.AddRange(items);
        }
    }
}
