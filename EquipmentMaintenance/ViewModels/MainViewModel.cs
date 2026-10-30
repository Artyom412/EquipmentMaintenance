using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentMaintenance.Services;

namespace EquipmentMaintenance.ViewModels;

public partial class MainViewModel(INavigationService navigationService) : ViewModelBase
{
    private readonly INavigationService _navigationService = navigationService;

    [ObservableProperty]
    public partial ObservableObject? CurrentViewModel { get; private set; }

    [RelayCommand]
    private void ApplicationShutdown()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    [RelayCommand]
    private void OpenEmployeeList() => CurrentViewModel = _navigationService.NavigateTo<EmployeeListViewModel>();

    [RelayCommand]
    private void OpenEquipmentList() => CurrentViewModel = _navigationService.NavigateTo<EquipmentListViewModel>();

    [RelayCommand]
    private void OpenOthersList() => CurrentViewModel = _navigationService.NavigateTo<OthersListViewModel>();

    [RelayCommand]
    private void OpenSubdivisionList() => CurrentViewModel = _navigationService.NavigateTo<SubdivisionListViewModel>();
}