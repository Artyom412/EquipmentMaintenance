using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class MaterialObjectViewModel(ICrudService crudService) : ObjectBaseView
    {
        private readonly ICrudService _crudService = crudService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string ItemNumber { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial UnitOfMeasurement? SelectedMeasure { get; set; }

        public AvaloniaList<UnitOfMeasurement> Measurements { get; set; } = [];

        [ObservableProperty]
        public partial int ConsumptionRate { get; set; }

        public Material? CreatedMaterial { get; set; }

        protected override bool CanExecute() => 
            !string.IsNullOrWhiteSpace(ItemNumber)
            && !string.IsNullOrWhiteSpace(Name)
            && SelectedMeasure is not null;

        public async Task LoadDataFromServer()
        {
            var units = await _crudService.GetRecordsAsync<UnitOfMeasurement>();
            Measurements.AddRange(units);
        }

        protected override void CancelExecute()
        {
            CreatedMaterial = null;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }

        protected override void ConfirmExecute()
        {
            var material = new Material
            {
                ItemNumber = ItemNumber,
                Name = Name,
                UnitOfMeasurementId = SelectedMeasure!.Id,
                ConsumptionRate = ConsumptionRate
            };

            CreatedMaterial = material;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }
    }
}
