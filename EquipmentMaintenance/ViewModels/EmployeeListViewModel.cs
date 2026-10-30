using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;

namespace EquipmentMaintenance.ViewModels
{
    public partial class EmployeeListViewModel(Func<EmployeeObjectViewModel> employeeVmFactory, ICrudService crudService) : ListViewBase<Employee>
    {
        private readonly Func<EmployeeObjectViewModel> _employeeVmFactory = employeeVmFactory;
        private readonly ICrudService _crudService = crudService;

        public async Task LoadDataFromServer()
        {
            var employees = await _crudService.GetRecordsAsync<Employee>(x => x.JobTitle, x => x.Subdivision);

            Items.AddRange(employees);
        }

        protected override async Task AddExecute()
        {
            var employeeViewModel = _employeeVmFactory();

            Employee? createdEmployee = await WeakReferenceMessenger.Default.Send(new EmployeeDialogMessage(employeeViewModel)).Response;

            if (createdEmployee is not null)
            {
                await _crudService.AddRecordAsync(createdEmployee);
                createdEmployee.JobTitle = employeeViewModel.SelectedJobTitle!;
                createdEmployee.Subdivision = employeeViewModel.SelectedSubdivision!;
                Items.Add(createdEmployee);
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
