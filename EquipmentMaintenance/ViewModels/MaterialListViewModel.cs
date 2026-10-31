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

        protected override async Task AddExecute()
        {
            var materialViewModel = _materialVmFactory();

            Material? createdMaterial = await WeakReferenceMessenger.Default.Send(new MaterialDialogMessage(materialViewModel)).Response;
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
