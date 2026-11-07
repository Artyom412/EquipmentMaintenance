using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class EmployeeObjectView : Window
    {
        public EmployeeObjectView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<EmployeeObjectView, CloseDialogMessage>(this, (recipient, message) =>
            {
                recipient.Close();
                WeakReferenceMessenger.Default.Unregister<CloseDialogMessage>(recipient);
            });

            Loaded += async (sender, e) =>
            {
                if (DataContext is EmployeeObjectViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}