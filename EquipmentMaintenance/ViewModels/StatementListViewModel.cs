using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class StatementListViewModel(ICrudService crudService, Func<StatementObjectViewModel> statementObjectVmFactory) : ListViewBase<Statement>
    {
        private readonly ICrudService _crudService = crudService;
        private readonly Func<StatementObjectViewModel> _statementObjectVmFactory = statementObjectVmFactory;

        public Statement? SelectedStatement { get; set; }

        public async Task LoadDataFromServer()
        {
            var statements = await _crudService.GetRecordsAsync<Statement>();
            Items.AddRange(statements);
        }

        protected async override Task AddExecute()
        {
            var viewModel = _statementObjectVmFactory();

            Statement? statement = await WeakReferenceMessenger.Default.Send(new StatementDialogMessage(viewModel)).Response;

            if (statement is not null)
            {
                await _crudService.AddRecordAsync(statement);
                statement.Subdivision = viewModel.SelectedSubdivision!;
                statement.Equipment = viewModel.SelectedEquipment!;
                statement.Employee = viewModel.SelectedEmployee!;
                statement.Material = viewModel.SelectedMaterial!;
                Items.Add(statement);
            }
        }

        protected async override Task DeleteExecute()
        {
            if (SelectedStatement is null) return;

            await _crudService.RemoveRecordAsync(SelectedStatement);
            Items.Remove(SelectedStatement);
            SelectedStatement = null;
        }

        protected async override Task RefreshExecute()
        {
            throw new NotImplementedException();
        }
    }
}
