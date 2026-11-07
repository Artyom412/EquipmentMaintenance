using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class StatementObjectView : Window
    {
        public StatementObjectView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<StatementObjectView, CloseDialogMessage>(this, (recipient, message) =>
            {
                recipient.Close();
                WeakReferenceMessenger.Default.Unregister<CloseDialogMessage>(recipient);
            });

            Loaded += async (senser, e) =>
            {
                if (DataContext is StatementObjectViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}