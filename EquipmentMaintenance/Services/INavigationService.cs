using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Services
{
    public interface INavigationService
    {
        ObservableObject NavigateTo<TViewModel>() where TViewModel : ObservableObject;
    }
}
