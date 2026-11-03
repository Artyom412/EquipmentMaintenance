using Avalonia.Collections;
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
    public class MaterialTransactionListViewModel(ICrudService crudService, Func<MaterialTransactionObjectViewModel> materialTransactionVmFactory) : ListViewBase<MaterialTransaction>
    {
        private readonly Func<MaterialTransactionObjectViewModel> _materialTransactionVmFactory = materialTransactionVmFactory;
        private readonly ICrudService _crudService = crudService;

        protected override async Task AddExecute()
        {
            var viewModel = _materialTransactionVmFactory();

            MaterialTransaction? materialTransaction = await WeakReferenceMessenger.Default.Send(new MaterialTransactionDialogMessage(viewModel)).Response;
        }

        public async Task LoadDataFromServer()
        {
            var materialTransactions = await _crudService.GetRecordsAsync<MaterialTransaction>();
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
