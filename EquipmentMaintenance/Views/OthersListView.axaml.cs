using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class OthersListView : UserControl
    {
        public OthersListView()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                if (DataContext is OthersListViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}