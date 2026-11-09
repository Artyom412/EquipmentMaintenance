using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class MaterialDialogMessage(MaterialObjectViewModel viewModel, Material? material) : AsyncRequestMessage<Material?>
    {
        public MaterialObjectViewModel ViewModel { get; } = viewModel;
        public Material? Material { get; } = material;
    }
}
