using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class MaterialTransactionListView : UserControl
    {
        public MaterialTransactionListView()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                if (DataContext is MaterialTransactionListViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}