using CommunityToolkit.Mvvm.Messaging.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Messages
{
    internal class MaterialDialogMessage(MaterialObjectViewModel viewModel) : AsyncRequestMessage<Material?>
    {
        public MaterialObjectViewModel ViewModel { get; } = viewModel;
    }
}
