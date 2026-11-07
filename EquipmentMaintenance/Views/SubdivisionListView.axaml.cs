using Avalonia.Controls;
using EquipmentMaintenance.ViewModels;

namespace EquipmentMaintenance.Views
{
    public partial class SubdivisionListView : UserControl
    {
        public SubdivisionListView()
        {
            InitializeComponent();

            Loaded += async (senser, e) =>
            {
                if (DataContext is SubdivisionListViewModel viewModel)
                {
                    await viewModel.LoadDataFromServer();
                }
            };
        }
    }
}