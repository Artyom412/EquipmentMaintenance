using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class EmployeeListView : UserControl
    {
        public EmployeeListView()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                if (DataContext is EmployeeListViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}