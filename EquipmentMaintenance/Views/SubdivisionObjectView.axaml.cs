using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;

namespace EquipmentMaintenance.Views
{
    public partial class SubdivisionObjectView : Window
    {
        public SubdivisionObjectView()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<SubdivisionObjectView, CloseDialogMessage>(this, (recipient, message) =>
            {
                recipient.Close();
                WeakReferenceMessenger.Default.Unregister<CloseDialogMessage>(recipient);
            });
        }
    }
}