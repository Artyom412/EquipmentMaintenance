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

        protected override async Task AddExecute()
        {
            Employee? createdEmployee = await WeakReferenceMessenger.Default.Send(new EmployeeDialogMessage(_employeeViewModel));

            if (createdEmployee is not null)
            {
                await _crudService.AddRecordAsync(createdEmployee);
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
