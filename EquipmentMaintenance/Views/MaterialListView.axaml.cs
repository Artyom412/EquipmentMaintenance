using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class MaterialListView : UserControl
    {
        public MaterialListView()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                if (DataContext is MaterialListViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}