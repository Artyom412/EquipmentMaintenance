using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class MaterialListViewModel(ICrudService crudService, Func<MaterialObjectViewModel> materialVmFactory) : ListViewBase<Material>
    {
        private readonly ICrudService _crudService = crudService;
        private readonly Func<MaterialObjectViewModel> _materialVmFactory = materialVmFactory;

        public Material? SelectedMaterial { get; set; }

        public async Task LoadDataFromServer()
        {
            await RunSafelyAsync(async () =>
            {
                var materials = await _crudService.GetRecordsAsync<Material>(x => x.UnitOfMeasurement);
                SetItems(materials);
            });
        }

        protected override async Task AddExecute()
        {
            var materialViewModel = _materialVmFactory();

            Material? createdMaterial = await WeakReferenceMessenger.Default.Send(new MaterialDialogMessage(materialViewModel, null)).Response;

            if (createdMaterial is not null)
            {
                await _crudService.AddRecordAsync(createdMaterial);
                createdMaterial.UnitOfMeasurement = materialViewModel.SelectedMeasure!;   
                AddItem(createdMaterial);
            }
        }

        protected override async Task DeleteExecute()
        {
            if (SelectedMaterial is null) return;

            await _crudService.RemoveRecordAsync(SelectedMaterial);
            RemoveItem(SelectedMaterial);
            SelectedMaterial = null;
        }

        protected override async Task RefreshExecute()
        {
            var materialViewModel = _materialVmFactory();
            Material? updatedMaterial = await WeakReferenceMessenger.Default.Send(new MaterialDialogMessage(materialViewModel, SelectedMaterial)).Response;

            if (updatedMaterial is not null)
            {
                updatedMaterial.UnitOfMeasurement = null!;
                await _crudService.UpdateRecordAsync(updatedMaterial);
                Items.Clear();
                await LoadDataFromServer();
            }
        }

        protected override bool MatchesSearch(Material material, string searchText) =>
            material.ItemNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase)
            || material.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
            || material.UnitOfMeasurement?.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true;
    }
}
