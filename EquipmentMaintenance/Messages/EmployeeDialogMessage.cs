using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class EmployeeDialogMessage(EmployeeObjectViewModel viewModel) : AsyncRequestMessage<Employee?>
    {
        public EmployeeObjectViewModel ViewModel { get; } = viewModel;
    }
}
