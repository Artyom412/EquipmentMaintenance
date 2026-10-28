using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using EquipmentMaintenance.Services;
using Microsoft.EntityFrameworkCore;
using EquipmentMaintenance.Models;
using Avalonia.Collections;

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
        public partial EquipmentType? EquipmentType { get; set; }
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string Model { get; set; } = string.Empty;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Location? SelectedLocation { get; set; }
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial DateOnly DateOfCommissioning { get; set; }

        public AvaloniaList<Location> Locations { get; } = [];

        public Equipment? CreatedEquipment { get; private set; }

        protected override bool CanExecute() =>
            !string.IsNullOrWhiteSpace(Number)
            && string.IsNullOrWhiteSpace(Model)
            && EquipmentType.HasValue
            && SelectedLocation is not null
            && DateOfCommissioning >= DateOnly.FromDateTime(DateTime.Now);


        protected override void CancelExecute()
        {
            CreatedEquipment = null;
        }

        protected override void ConfirmExecute()
        {
            var equipment = new Equipment
            {
                InventoryNumber = Number,
                EquipmentType = EquipmentType ?? Models.EquipmentType.Other,
                Model = Model,
                LocationId = SelectedLocation!.Id,
                DateOfCommissioning = DateOfCommissioning
            };

            CreatedEquipment = equipment;
        }
    }
}
