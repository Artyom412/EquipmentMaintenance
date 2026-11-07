using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class EquipmentObjectView : Window
    {
        public EquipmentObjectView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<EquipmentObjectView, CloseDialogMessage>(this, (recipient, message) =>
            {
                recipient.Close();
                WeakReferenceMessenger.Default.Unregister<CloseDialogMessage>(recipient);
            });

            Loaded += async (sender, e) =>
            {
                if (DataContext is EquipmentObjectViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}