using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Messages
{
    internal class SubdivisionDialogMessage(SubdivisionObjectViewModel viewModel) : AsyncRequestMessage<Subdivision?>
    {
        public SubdivisionObjectViewModel ViewModel { get; } = viewModel;
    }
}
