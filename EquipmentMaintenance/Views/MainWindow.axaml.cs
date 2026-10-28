using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<MainWindow, EmployeeDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogAsync(message.ViewModel));
        });
    }

    private async Task<Employee?> OpenDialogAsync(EmployeeObjectViewModel dialogViewModel)
    {
        var viewLocator = Application.Current?.DataTemplates.OfType<ViewLocator>().FirstOrDefault();
        var view = viewLocator?.Build(dialogViewModel);

        if (view is not Window dialogWindow)
        {
            return null;
        }

        dialogWindow.DataContext = dialogViewModel;
        dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        await dialogWindow.ShowDialog(this);

        return dialogViewModel.CreatedEmployee;
    }
}