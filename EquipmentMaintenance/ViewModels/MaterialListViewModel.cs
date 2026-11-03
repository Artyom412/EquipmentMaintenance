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
    public partial class MaterialListViewModel(ICrudService crudService, Func<MaterialObjectViewModel> materialVmFactory) : ListViewBase<Material>
    {
        private readonly ICrudService _crudService = crudService;
        private readonly Func<MaterialObjectViewModel> _materialVmFactory = materialVmFactory;

        public async Task LoadDataFromServer()
        {
            var materials = await _crudService.GetRecordsAsync<Material>(x => x.UnitOfMeasurement);
            Items.AddRange(materials);
        }

        protected override async Task AddExecute()
        {
            var materialViewModel = _materialVmFactory();

            Material? createdMaterial = await WeakReferenceMessenger.Default.Send(new MaterialDialogMessage(materialViewModel)).Response;

            if (createdMaterial is not null)
            {
                await _crudService.AddRecordAsync(createdMaterial);
                createdMaterial.UnitOfMeasurement = materialViewModel.SelectedMeasure!;   
                Items.Add(createdMaterial);
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
