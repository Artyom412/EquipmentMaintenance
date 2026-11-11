using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class StatementDialogMessage(StatementObjectViewModel viewModel, Statement? statement) : AsyncRequestMessage<Statement?>
    {
        public StatementObjectViewModel ViewModel { get; } = viewModel;

        public Statement? Statement { get; } = statement;
    }
}
