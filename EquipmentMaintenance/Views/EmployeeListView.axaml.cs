using Avalonia.Controls;
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