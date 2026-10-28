using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class EquipmentObjectView : Window
    {
        public EquipmentObjectView()
        {
            InitializeComponent();

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