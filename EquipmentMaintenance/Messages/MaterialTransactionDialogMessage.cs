using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class MaterialTransactionDialogMessage(MaterialTransactionObjectViewModel viewModel) : AsyncRequestMessage<MaterialTransaction?>
    {
        public MaterialTransactionObjectViewModel ViewModel { get; } = viewModel;
    }
}
