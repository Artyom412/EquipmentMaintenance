using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class StatementListViewModel(
        ICrudService crudService,
        IStatementService statementService,
        Func<StatementObjectViewModel> statementObjectVmFactory) : ListViewBase<Statement>
    {
        private readonly ICrudService _crudService = crudService;
        private readonly IStatementService _statementService = statementService;
        private readonly Func<StatementObjectViewModel> _statementObjectVmFactory = statementObjectVmFactory;

        public Statement? SelectedStatement { get; set; }

        public async Task LoadDataFromServer()
        {
            await RunSafelyAsync(async () =>
            {
                var statements = await _crudService.GetRecordsAsync<Statement>();
                SetItems(statements);
            });
        }

        protected async override Task AddExecute()
        {
            var viewModel = _statementObjectVmFactory();

            Statement? statement = await WeakReferenceMessenger.Default.Send(new StatementDialogMessage(viewModel, null)).Response;

            if (statement is not null)
            {
                await _statementService.SaveAsync(statement);
                statement.Subdivision = viewModel.SelectedSubdivision!;
                statement.Equipment = viewModel.SelectedEquipment!;
                statement.Employee = viewModel.SelectedEmployee!;
                statement.Material = viewModel.SelectedMaterial!;
                AddItem(statement);
            }
        }

        protected async override Task DeleteExecute()
        {
            if (SelectedStatement is null) return;

            await _statementService.DeleteAsync(SelectedStatement);
            RemoveItem(SelectedStatement);
            SelectedStatement = null;
        }

        protected async override Task RefreshExecute()
        {
            if (SelectedStatement is null) return;

            var viewModel = _statementObjectVmFactory();

            Statement? updatedStatement = await WeakReferenceMessenger.Default.Send(new StatementDialogMessage(viewModel, SelectedStatement)).Response;

            if (updatedStatement is not null)
            {
                await _statementService.SaveAsync(updatedStatement);
                await LoadDataFromServer();
            }
        }

        protected override bool MatchesSearch(Statement statement, string searchText) =>
            statement.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
            || statement.Description?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true
            || statement.Result?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true
            || statement.PriorityDescription.Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }
}
