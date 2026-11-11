using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public class MaterialTransactionListViewModel(ICrudService crudService, Func<MaterialTransactionObjectViewModel> materialTransactionVmFactory) : ListViewBase<MaterialTransaction>
    {
        private readonly Func<MaterialTransactionObjectViewModel> _materialTransactionVmFactory = materialTransactionVmFactory;
        private readonly ICrudService _crudService = crudService;

        public MaterialTransaction? SelectedMaterialTransaction { get; set; }

        protected override async Task AddExecute()
        {
            var viewModel = _materialTransactionVmFactory();

            MaterialTransaction? materialTransaction = await WeakReferenceMessenger.Default.Send(new MaterialTransactionDialogMessage(viewModel, null)).Response;

            if (materialTransaction is not null)
            {
                await _crudService.AddRecordAsync(materialTransaction);
                Items.Add(materialTransaction);
                materialTransaction.Material = viewModel.SelectedMaterial!;
            }
        }

        public async Task LoadDataFromServer()
        {
            Items.Clear();

            var materialTransactions = await _crudService.GetRecordsAsync<MaterialTransaction>(x => x.Material);
            Items.AddRange(materialTransactions);
        }

        protected async override Task DeleteExecute()
        {
            if (SelectedMaterialTransaction is null) return;

            await _crudService.RemoveRecordAsync(SelectedMaterialTransaction);
            Items.Remove(SelectedMaterialTransaction);
            SelectedMaterialTransaction = null;
        }

        protected async override Task RefreshExecute()
        {
            var viewModel = _materialTransactionVmFactory();

            MaterialTransaction? updatedMaterialTransaction = await WeakReferenceMessenger.Default.Send(new MaterialTransactionDialogMessage(viewModel, SelectedMaterialTransaction)).Response;

            if (updatedMaterialTransaction is not null)
            {
                await _crudService.UpdateRecordAsync(updatedMaterialTransaction);
                await LoadDataFromServer();
            }
        }
    }
}
