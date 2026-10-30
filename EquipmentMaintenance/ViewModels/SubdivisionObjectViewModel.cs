using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EquipmentMaintenance.ViewModels
{
    public partial class SubdivisionObjectViewModel : ObjectBaseView
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string Code { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string Name { get; set; } = string.Empty;

        public Subdivision? CreatedSubdivision { get; private set; }

        protected override bool CanExecute() =>
            !string.IsNullOrWhiteSpace(Code)
            && !string.IsNullOrWhiteSpace(Name);

        protected override void CancelExecute()
        {
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }

        protected override void ConfirmExecute()
        {
            var subdivision = new Subdivision
            {
                Code = Code,
                Name = Name
            };

            CreatedSubdivision = subdivision;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }
    }
}
