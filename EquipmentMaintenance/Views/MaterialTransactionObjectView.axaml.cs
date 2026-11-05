using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class MaterialTransactionObjectView : Window
    {
        public MaterialTransactionObjectView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<MaterialTransactionObjectView, CloseDialogMessage>(this, (recipient, message) =>
            {
                recipient.Close();
                WeakReferenceMessenger.Default.Unregister<CloseDialogMessage>(recipient);
            });

            Loaded += async (senser, e) =>
            {
                if (DataContext is MaterialTransactionObjectViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}