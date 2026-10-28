using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class EmployeeObjectView : Window
    {
        public EmployeeObjectView()
        {
            InitializeComponent();

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