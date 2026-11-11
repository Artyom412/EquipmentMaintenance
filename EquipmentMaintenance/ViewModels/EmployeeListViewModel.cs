using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class EmployeeListViewModel(Func<EmployeeObjectViewModel> employeeVmFactory, ICrudService crudService) : ListViewBase<Employee>
    {
        private readonly Func<EmployeeObjectViewModel> _employeeVmFactory = employeeVmFactory;
        private readonly ICrudService _crudService = crudService;

        public Employee? SelectedEmployee { get; set; }

        public async Task LoadDataFromServer()
        {
            await RunSafelyAsync(async () =>
            {
                var employees = await _crudService.GetRecordsAsync<Employee>(x => x.JobTitle, x => x.Subdivision);
                SetItems(employees);
            });
        }

        protected override async Task AddExecute()
        {
            var employeeViewModel = _employeeVmFactory();

            Employee? createdEmployee = await WeakReferenceMessenger.Default.Send(new EmployeeDialogMessage(employeeViewModel, null)).Response;

            if (createdEmployee is not null)
            {
                await _crudService.AddRecordAsync(createdEmployee);
                createdEmployee.JobTitle = employeeViewModel.SelectedJobTitle!;
                createdEmployee.Subdivision = employeeViewModel.SelectedSubdivision!;
                AddItem(createdEmployee);
            }
        }

        protected override async Task DeleteExecute()
        {
            if (SelectedEmployee is null) return;

            await _crudService.RemoveRecordAsync(SelectedEmployee);
            RemoveItem(SelectedEmployee);
            SelectedEmployee = null;
        }

        protected override async Task RefreshExecute()
        {
            var employeeViewModel = _employeeVmFactory();
            Employee? updatedEmployee = await WeakReferenceMessenger.Default.Send(new EmployeeDialogMessage(employeeViewModel, SelectedEmployee)).Response;

            if (updatedEmployee is not null)
            {
                updatedEmployee.JobTitle = null!;
                updatedEmployee.Subdivision = null!;
                await _crudService.UpdateRecordAsync(updatedEmployee);
                Items.Clear();
                await LoadDataFromServer();
            }
        }

        protected override bool MatchesSearch(Employee employee, string searchText) =>
            employee.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
            || employee.JobTitle?.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true
            || employee.Subdivision?.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true;
    }
}
