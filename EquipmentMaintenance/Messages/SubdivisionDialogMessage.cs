using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Messages
{
    internal class SubdivisionDialogMessage(SubdivisionObjectViewModel viewModel, Subdivision? subdivision) : AsyncRequestMessage<Subdivision?>
    {
        public SubdivisionObjectViewModel ViewModel { get; } = viewModel;
        public Subdivision? Subdivision { get; } = subdivision;
    }
}
