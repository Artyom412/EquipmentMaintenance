using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class EquipmentListViewModel(Func<EquipmentObjectViewModel> equipmentVmFactory, ICrudService crudService) : ListViewBase<Equipment>
    {
        private readonly Func<EquipmentObjectViewModel> _equipmentVmFactory = equipmentVmFactory;
        private readonly ICrudService _crudService = crudService;

        protected override async Task AddExecute()
        {
            var employeeViewModel = _equipmentVmFactory();

            Equipment? createdEquipment = await WeakReferenceMessenger.Default.Send(new EquipmentDialogMessage(employeeViewModel));

            if (createdEquipment is not null)
            {
                await _crudService.AddRecordAsync(createdEquipment);
                createdEquipment.Location = employeeViewModel.SelectedLocation!;
                Items.Add(createdEquipment);
            }
        }

        public async Task LoadDataFromServer()
        {
            var equipments = await _crudService.GetRecordsAsync<Equipment>();
            Items.AddRange(equipments);
        }

        protected override void DeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override void RefreshExecute()
        {
            throw new NotImplementedException();
        }
    }
}
