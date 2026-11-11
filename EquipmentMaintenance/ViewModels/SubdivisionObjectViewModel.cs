using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using System.Threading.Tasks;

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

        public Subdivision? CreatedSubdivision { get; set; }

        public async Task LoadDataFromServer()
        {
            if (CreatedSubdivision is not null)
            {
                Name = CreatedSubdivision.Name;
                Code = CreatedSubdivision.Code;
            }
        }

        protected override bool CanExecute() =>
            !string.IsNullOrWhiteSpace(Code)
            && !string.IsNullOrWhiteSpace(Name);

        protected override void CancelExecute()
        {
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }

        protected override void ConfirmExecute()
        {
            if (CreatedSubdivision is not null)
            {
                CreatedSubdivision.Name = Name;
                CreatedSubdivision.Code = Code;

                WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
            }
            else
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
}
