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
    public partial class EquipmentListViewModel(EquipmentObjectViewModel viewModel, ICrudService crudService) : ListViewBase<Equipment>
    {
        private readonly EquipmentObjectViewModel _equipmentViewModel = viewModel;
        private readonly ICrudService _crudService = crudService;

        protected override async Task AddExecute()
        {
            Equipment? createdEquipment = await WeakReferenceMessenger.Default.Send(new EquipmentDialogMessage(_equipmentViewModel));

            if (createdEquipment is not null)
            {
                await _crudService.AddRecordAsync(createdEquipment);
            }
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
