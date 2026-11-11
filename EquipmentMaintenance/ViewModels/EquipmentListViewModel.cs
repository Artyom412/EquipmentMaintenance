using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class EquipmentListViewModel(Func<EquipmentObjectViewModel> equipmentVmFactory, ICrudService crudService) : ListViewBase<Equipment>
    {
        private readonly Func<EquipmentObjectViewModel> _equipmentVmFactory = equipmentVmFactory;
        private readonly ICrudService _crudService = crudService;

        public Equipment? SelectedEquipment { get; set; }

        protected override async Task AddExecute()
        {
            var employeeViewModel = _equipmentVmFactory();

            Equipment? createdEquipment = await WeakReferenceMessenger.Default.Send(new EquipmentDialogMessage(employeeViewModel, null)).Response;

            if (createdEquipment is not null)
            {
                await _crudService.AddRecordAsync(createdEquipment);
                createdEquipment.Location = employeeViewModel.SelectedLocation!;
                AddItem(createdEquipment);
            }
        }

        public async Task LoadDataFromServer()
        {
            await RunSafelyAsync(async () =>
            {
                var equipments = await _crudService.GetRecordsAsync<Equipment>(x => x.Location);
                SetItems(equipments);
            });
        }

        protected async override Task DeleteExecute()
        {
            if (SelectedEquipment is null) return;

            await _crudService.RemoveRecordAsync(SelectedEquipment);
            RemoveItem(SelectedEquipment);
            SelectedEquipment = null;
        }

        protected async override Task RefreshExecute()
        {
            if (SelectedEquipment is null) return;

            var employeeViewModel = _equipmentVmFactory();
            Equipment? createdEquipment = await WeakReferenceMessenger.Default.Send(new EquipmentDialogMessage(employeeViewModel, SelectedEquipment)).Response;

            if (createdEquipment is not null)
            {
                createdEquipment.Location = null!;
                await _crudService.UpdateRecordAsync(createdEquipment);
                Items.Clear();
                await LoadDataFromServer();
            }
        }

        protected override bool MatchesSearch(Equipment equipment, string searchText) =>
            equipment.InventoryNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase)
            || equipment.Model.Contains(searchText, StringComparison.OrdinalIgnoreCase)
            || equipment.Location?.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true
            || equipment.EquipmentType.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }
}
