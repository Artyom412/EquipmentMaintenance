using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public class MaterialTransactionListViewModel(ICrudService crudService, Func<MaterialTransactionObjectViewModel> materialTransactionVmFactory) : ListViewBase<MaterialTransaction>
    {
        private readonly Func<MaterialTransactionObjectViewModel> _materialTransactionVmFactory = materialTransactionVmFactory;
        private readonly ICrudService _crudService = crudService;

        protected override async Task AddExecute()
        {
            var viewModel = _materialTransactionVmFactory();

            MaterialTransaction? materialTransaction = await WeakReferenceMessenger.Default.Send(new MaterialTransactionDialogMessage(viewModel)).Response;

            if (materialTransaction is not null)
            {
                await _crudService.AddRecordAsync(materialTransaction);
                Items.Add(materialTransaction);
                materialTransaction.Material = viewModel.SelectedMaterial!;
            }
        }

        public async Task LoadDataFromServer()
        {
            var materialTransactions = await _crudService.GetRecordsAsync<MaterialTransaction>(x => x.Material);
            Items.AddRange(materialTransactions);
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
