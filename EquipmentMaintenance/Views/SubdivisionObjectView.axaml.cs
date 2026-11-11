using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class SubdivisionObjectView : Window
    {
        public SubdivisionObjectView()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                if (DataContext is SubdivisionObjectViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };

            WeakReferenceMessenger.Default.Register<SubdivisionObjectView, CloseDialogMessage>(this, (recipient, message) =>
            {
                recipient.Close();
                WeakReferenceMessenger.Default.Unregister<CloseDialogMessage>(recipient);
            });
        }
    }
}