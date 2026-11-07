using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class StatementDialogMessage(StatementObjectViewModel viewModel) : AsyncRequestMessage<Statement?>
    {
        public StatementObjectViewModel ViewModel { get; } = viewModel;
    }
}
