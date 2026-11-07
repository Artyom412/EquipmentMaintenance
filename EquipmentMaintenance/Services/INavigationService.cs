using CommunityToolkit.Mvvm.ComponentModel;

namespace EquipmentMaintenance.Services
{
    public interface INavigationService
    {
        ObservableObject NavigateTo<TViewModel>() where TViewModel : ObservableObject;
    }
}
