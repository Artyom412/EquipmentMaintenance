using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EquipmentMaintenance.ViewModels
{
    public abstract partial class ObjectBaseView : ObservableObject
    {
        [RelayCommand(CanExecute = nameof(CanExecute))]
        protected virtual void ConfirmExecute()
        {

        }

        protected virtual bool CanExecute()
        {
            return true;
        }

        [RelayCommand]
        protected virtual void CancelExecute()
        {

        }
    }
}
