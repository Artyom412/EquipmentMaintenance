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
    public partial class EmployeeListViewModel(EmployeeObjectViewModel employeeViewModel, ICrudService crudService) : ListViewBase<Employee>
    {
        private readonly EmployeeObjectViewModel _employeeViewModel = employeeViewModel;
        private readonly ICrudService _crudService = crudService;

        public async Task LoadDataFromServer()
        {
            var employees = await _crudService.GetRecordsAsync<Employee>(x => x.JobTitle);
            Items.AddRange(employees);
        }

        protected override async Task AddExecute()
        {
            Employee? createdEmployee = await WeakReferenceMessenger.Default.Send(new EmployeeDialogMessage(_employeeViewModel)).Response;

            if (createdEmployee is not null)
            {
                await _crudService.AddRecordAsync(createdEmployee);
                createdEmployee.JobTitle = _employeeViewModel.SelectedJobTitle!;
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
