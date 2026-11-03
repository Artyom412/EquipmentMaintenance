using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace EquipmentMaintenance.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<MainWindow, EmployeeDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogEmployeeAsync(message.ViewModel));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, EquipmentDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogEquipmentAsync(message.ViewModel));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, SubdivisionDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogSubdivisionAsync(message.ViewModel));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, MaterialDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogMaterialAsync(message.ViewModel));
        });
    }

    private async Task<Material?> OpenDialogMaterialAsync(MaterialObjectViewModel dialogViewModel)
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

        return dialogViewModel.CreatedMaterial;
    }

    private async Task<Equipment?> OpenDialogEquipmentAsync(EquipmentObjectViewModel dialogViewModel)
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

        return dialogViewModel.CreatedEquipment;
    }

    private async Task<Employee?> OpenDialogEmployeeAsync(EmployeeObjectViewModel dialogViewModel)
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

    private async Task<Subdivision?> OpenDialogSubdivisionAsync(SubdivisionObjectViewModel dialogViewModel)
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

        return dialogViewModel.CreatedSubdivision;
    }

}