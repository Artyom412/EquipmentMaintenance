using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class EquipmentListView : UserControl
    {
        public EquipmentListView()
        {
            InitializeComponent();

            Loaded += async (senser, e) =>
            {
                if (DataContext is EquipmentListViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}