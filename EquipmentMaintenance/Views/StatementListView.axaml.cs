using Avalonia.Controls;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class StatementListView : UserControl
    {
        public StatementListView()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                if (DataContext is StatementListViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}