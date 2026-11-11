using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class MaterialTransactionObjectViewModel(ICrudService crudService) : ObjectBaseView
    {
        private readonly ICrudService _crudService = crudService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial DateTimeOffset? Date { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Material? SelectedMaterial { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial TransationOption? SelectedType { get; set; }

        [ObservableProperty]
        public partial int Quantity { get; set; }

        public MaterialTransaction? CreatedMaterialTransaction { get; set; }

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

            if (CreatedMaterialTransaction is not null)
            {
                Date = CreatedMaterialTransaction.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Local);
                Quantity = CreatedMaterialTransaction.Quantity;
                SelectedMaterial = Materials.First(x => x.Id == CreatedMaterialTransaction.MaterialId);
                SelectedType = TransationOptions.First(x => x.ValueType == CreatedMaterialTransaction.TransactionType);
            }
        }

        protected override void CancelExecute()
        {
            CreatedMaterialTransaction = null;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }

        protected override void ConfirmExecute()
        {
            if (CreatedMaterialTransaction is not null)
            {
                CreatedMaterialTransaction.Date = DateOnly.FromDateTime(Date!.Value.Date);
                CreatedMaterialTransaction.Quantity = Quantity;
                CreatedMaterialTransaction.MaterialId = SelectedMaterial!.Id;
                CreatedMaterialTransaction.TransactionType = SelectedType!.ValueType;

                WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
            }
            else
            {
                var materialTransaction = new MaterialTransaction
                {
                    Date = DateOnly.FromDateTime(Date!.Value.Date),
                    MaterialId = SelectedMaterial!.Id,
                    TransactionType = SelectedType!.ValueType,
                    Quantity = Quantity
                };

                CreatedMaterialTransaction = materialTransaction;
            }

            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }
    }

    public record TransationOption(TransactionType ValueType, string Label);
}
