using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class MaterialTransactionDialogMessage(MaterialTransactionObjectViewModel viewModel, MaterialTransaction? materialTransaction) : AsyncRequestMessage<MaterialTransaction?>
    {
        public MaterialTransactionObjectViewModel ViewModel { get; } = viewModel;
        public MaterialTransaction? MaterialTransaction { get; } = materialTransaction;
    }
}
