using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class MaterialObjectView : Window
    {
        public MaterialObjectView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<MaterialObjectView, CloseDialogMessage>(this, (recipient, message) =>
            {
                recipient.Close();
                WeakReferenceMessenger.Default.Unregister<CloseDialogMessage>(recipient);
            });

            Loaded += async (senser, e) =>
            {
                if (DataContext is MaterialObjectViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}