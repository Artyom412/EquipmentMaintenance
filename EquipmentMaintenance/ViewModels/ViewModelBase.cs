using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    private string? _errorMessage;

    public string? ErrorMessage
    {
        get => _errorMessage;
        private set => SetProperty(ref _errorMessage, value);
    }

    protected async Task RunSafelyAsync(Func<Task> action)
    {
        ErrorMessage = null;

        try
        {
            await action();
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
            ErrorMessage = exception is InvalidOperationException
                ? exception.Message
                : "Не удалось выполнить операцию. Проверьте данные и повторите попытку.";
        }
    }
}
