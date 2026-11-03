using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EquipmentMaintenance.ViewModels
{
    public partial class MaterialTransactionObjectViewModel(ICrudService crudService) : ObjectBaseView
    {
        private readonly ICrudService _crudService = crudService;

        [ObservableProperty]
        public partial DateTimeOffset? Date { get; set; }

        [ObservableProperty]
        public partial Material? SelectedMaterial { get; set; }

        [ObservableProperty]
        public partial TransactionType? SelectedType { get; set; }

        [ObservableProperty]
        public partial int Quantity { get; set; }

        public AvaloniaList<Material> Materials { get; set; } = [];

        public TransationOption[] TransationOptions { get; } =
        [
            new TransationOption(TransactionType.Income, "Приход"),
            new TransationOption(TransactionType.Expense, "Расход")
        ];

        protected override bool CanExecute() => 
            Date is not null
            && SelectedMaterial is not null
            && SelectedType is not null;


        public async Task LoadDataFromServer()
        {
            var materials = await _crudService.GetRecordsAsync<Material>();
            Materials.AddRange(materials);
        }

        protected override void CancelExecute()
        {
            
        }

        protected override void ConfirmExecute()
        {
            
        }
    }

    public record TransationOption(TransactionType ValueType, string Label);
}
