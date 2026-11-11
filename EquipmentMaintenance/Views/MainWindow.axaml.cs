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
            message.Reply(recipient.OpenDialogEmployeeAsync(message.ViewModel, message.Employee));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, EquipmentDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogEquipmentAsync(message.ViewModel, message.Equipment));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, SubdivisionDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogSubdivisionAsync(message.ViewModel, message.Subdivision));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, MaterialDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogMaterialAsync(message.ViewModel, message.Material));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, MaterialTransactionDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogMaterialTransactionAsync(message.ViewModel, message.MaterialTransaction));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, StatementDialogMessage>(this, (recipient, message) =>
        {
            message.Reply(recipient.OpenDialogStatementAsync(message.ViewModel, message.Statement));
        });
    }

    private async Task<Statement?> OpenDialogStatementAsync(StatementObjectViewModel dialogViewModel, Statement? statement)
    {
        var viewLocator = Application.Current?.DataTemplates.OfType<ViewLocator>().FirstOrDefault();
        var view = viewLocator?.Build(dialogViewModel);

        if (view is not Window dialogWindow)
        {
            return null;
        }

        if (statement is not null)
        {
            var statementCopy = new Statement
            {
                Id = statement.Id,
                Name = statement.Name,
                Arrival = statement.Arrival,
                SubdivisionId = statement.SubdivisionId,
                EquipmentId = statement.EquipmentId,
                Description = statement.Description,
                Priority = statement.Priority,
                EmployeeId = statement.EmployeeId,
                CompletionDate = statement.CompletionDate,
                MaterialId = statement.MaterialId,
                Result = statement.Result,
            };

            dialogViewModel.CreatedStatement = statementCopy;
        }

        dialogWindow.DataContext = dialogViewModel;
        dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        await dialogWindow.ShowDialog(this);

        return dialogViewModel.CreatedStatement;
    }

    private async Task<MaterialTransaction?> OpenDialogMaterialTransactionAsync(MaterialTransactionObjectViewModel dialogViewModel, MaterialTransaction? materialTransaction)
    {
        var viewLocator = Application.Current?.DataTemplates.OfType<ViewLocator>().FirstOrDefault();
        var view = viewLocator?.Build(dialogViewModel);

        if (view is not Window dialogWindow)
        {
            return null;
        }

        if (materialTransaction is not null)
        {
            var materialTransactionCopy = new MaterialTransaction
            {
                Id = materialTransaction.Id,
                Date = materialTransaction.Date,
                MaterialId = materialTransaction.MaterialId,
                TransactionType = materialTransaction.TransactionType,
                Quantity = materialTransaction.Quantity,
            };

            dialogViewModel.CreatedMaterialTransaction = materialTransactionCopy;
        }

        dialogWindow.DataContext = dialogViewModel;
        dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        await dialogWindow.ShowDialog(this);

        return dialogViewModel.CreatedMaterialTransaction;
    }

    private async Task<Material?> OpenDialogMaterialAsync(MaterialObjectViewModel dialogViewModel, Material? material)
    {
        var viewLocator = Application.Current?.DataTemplates.OfType<ViewLocator>().FirstOrDefault();
        var view = viewLocator?.Build(dialogViewModel);

        if (view is not Window dialogWindow)
        {
            return null;
        }

        if (material is not null)
        {
            var materialCopy = new Material
            {
                Id = material.Id,
                ItemNumber = material.ItemNumber,
                Name = material.Name,
                UnitOfMeasurementId = material.UnitOfMeasurementId,
                ConsumptionRate = material.ConsumptionRate,
            };

            dialogViewModel.CreatedMaterial = materialCopy;
        }

        dialogWindow.DataContext = dialogViewModel;
        dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        await dialogWindow.ShowDialog(this);

        return dialogViewModel.CreatedMaterial;
    }

    private async Task<Equipment?> OpenDialogEquipmentAsync(EquipmentObjectViewModel dialogViewModel, Equipment? equipment)
    {
        var viewLocator = Application.Current?.DataTemplates.OfType<ViewLocator>().FirstOrDefault();
        var view = viewLocator?.Build(dialogViewModel);

        if (view is not Window dialogWindow)
        {
            return null;
        }

        if (equipment is not null)
        {
            var equipmentCopy = new Equipment
            {
                Id = equipment.Id,
                InventoryNumber = equipment.InventoryNumber,
                EquipmentType = equipment.EquipmentType,
                Model = equipment.Model,
                LocationId = equipment.LocationId,
                DateOfCommissioning = equipment.DateOfCommissioning,
            };

            dialogViewModel.CreatedEquipment = equipmentCopy;
        }

        dialogWindow.DataContext = dialogViewModel;
        dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        await dialogWindow.ShowDialog(this);

        return dialogViewModel.CreatedEquipment;
    }

    private async Task<Employee?> OpenDialogEmployeeAsync(EmployeeObjectViewModel dialogViewModel, Employee? employee)
    {
        var viewLocator = Application.Current?.DataTemplates.OfType<ViewLocator>().FirstOrDefault();
        var view = viewLocator?.Build(dialogViewModel);

        if (view is not Window dialogWindow)
        {
            return null;
        }

        if (employee is not null)
        {
            var employeeCopy = new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                SubdivisionId = employee.SubdivisionId,
                JobTitleId = employee.JobTitleId,
            };

            dialogViewModel.CreatedEmployee = employeeCopy;
        }

        dialogWindow.DataContext = dialogViewModel;
        dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        await dialogWindow.ShowDialog(this);

        return dialogViewModel.CreatedEmployee;
    }

    private async Task<Subdivision?> OpenDialogSubdivisionAsync(SubdivisionObjectViewModel dialogViewModel, Subdivision? subdivision)
    {
        var viewLocator = Application.Current?.DataTemplates.OfType<ViewLocator>().FirstOrDefault();
        var view = viewLocator?.Build(dialogViewModel);

        if (view is not Window dialogWindow)
        {
            return null;
        }

        if (subdivision is not null)
        {
            var subdivisionCopy = new Subdivision
            {
                Id = subdivision.Id,
                Name = subdivision.Name,
                Code = subdivision.Code,
            };

            dialogViewModel.CreatedSubdivision = subdivisionCopy;
        }

        dialogWindow.DataContext = dialogViewModel;
        dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        await dialogWindow.ShowDialog(this);

        return dialogViewModel.CreatedSubdivision;
    }
}