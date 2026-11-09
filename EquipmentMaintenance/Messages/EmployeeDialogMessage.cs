using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class EmployeeDialogMessage(EmployeeObjectViewModel viewModel, Employee? employee) : AsyncRequestMessage<Employee?>
    {
        public EmployeeObjectViewModel ViewModel { get; } = viewModel;
        public Employee? Employee { get; } = employee;
    }
}
