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
    public partial class EquipmentObjectViewModel(ICrudService crudService) : ObjectBaseView
    {
        private readonly ICrudService _crudService = crudService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string Number { get; set; } = string.Empty;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial EquipmentOption? SelectedEquipmentType { get; set; }
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string Model { get; set; } = string.Empty;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Location? SelectedLocation { get; set; }
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial DateTimeOffset? DateOfCommissioning { get; set; }

        public AvaloniaList<Location> Locations { get; } = [];
        public EquipmentOption[] EquipmentOptions { get; } =
        [
            new EquipmentOption(EquipmentType.Printer, "Принтер"),
            new EquipmentOption(EquipmentType.MultifunctionPrinter, "МФУ"),
            new EquipmentOption(EquipmentType.Laptop, "Ноутбук"),
            new EquipmentOption(EquipmentType.Computer, "Компьютер"),
            new EquipmentOption(EquipmentType.Monitor, "Монитор"),
            new EquipmentOption(EquipmentType.Speakers, "Колонки"),
            new EquipmentOption(EquipmentType.Other, "Прочее")
        ];

        public Equipment? CreatedEquipment { get; set; }

        public async Task LoadDataFromServer()
        {
            var locations = await _crudService.GetRecordsAsync<Location>();
            Locations.AddRange(locations);

            if (CreatedEquipment is not null)
            {
                Number = CreatedEquipment.InventoryNumber;
                SelectedEquipmentType = EquipmentOptions.Where(x => x.ValueType == CreatedEquipment.EquipmentType).First();
                Model = CreatedEquipment.Model;
                SelectedLocation = Locations.Where(x => x.Id == CreatedEquipment.Location.Id).FirstOrDefault();
                DateOfCommissioning = CreatedEquipment.DateOfCommissioning.ToDateTime(TimeOnly.MinValue, DateTimeKind.Local);
            }
        }

        protected override bool CanExecute() =>
            !string.IsNullOrWhiteSpace(Number)
            && !string.IsNullOrWhiteSpace(Model)
            && SelectedEquipmentType is not null
            && SelectedLocation is not null
            && DateOfCommissioning is not null;

        protected override void CancelExecute()
        {
            CreatedEquipment = null;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }

        protected override void ConfirmExecute()
        {
            if (CreatedEquipment is not null)
            {
                CreatedEquipment.InventoryNumber = Number;
                CreatedEquipment.EquipmentType = SelectedEquipmentType?.ValueType ?? Models.EquipmentType.Other;
                CreatedEquipment.Model = Model;
                CreatedEquipment.LocationId = SelectedLocation!.Id;
                CreatedEquipment.DateOfCommissioning = DateOnly.FromDateTime(DateOfCommissioning!.Value.Date);

                WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
            }
            else
            {
                var equipment = new Equipment
                {
                    InventoryNumber = Number,
                    EquipmentType = SelectedEquipmentType?.ValueType ?? Models.EquipmentType.Other,
                    Model = Model,
                    LocationId = SelectedLocation!.Id,
                    DateOfCommissioning = DateOnly.FromDateTime(DateOfCommissioning!.Value.Date),
                };

                CreatedEquipment = equipment;
                WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
            }
        }
    }

    public record EquipmentOption(EquipmentType ValueType, string Label);
}
