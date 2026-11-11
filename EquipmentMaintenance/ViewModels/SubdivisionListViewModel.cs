using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class SubdivisionListViewModel(Func<SubdivisionObjectViewModel> subdivisionVmFactory, ICrudService crudService) : ListViewBase<Subdivision>
    {
        private readonly ICrudService _crudService = crudService;
        private readonly Func<SubdivisionObjectViewModel> _subdivisionVmFactory = subdivisionVmFactory;

        public Subdivision? SelectedSubdivision { get; set; }

        public async Task LoadDataFromServer()
        {
            await RunSafelyAsync(async () =>
            {
                var subdivisions = await _crudService.GetRecordsAsync<Subdivision>();
                SetItems(subdivisions);
            });
        }

        protected async override Task AddExecute()
        {
            var subdivisionViewModel = _subdivisionVmFactory();

            Subdivision? createdSubdivision = await WeakReferenceMessenger.Default.Send(new SubdivisionDialogMessage(subdivisionViewModel, null));

            if (createdSubdivision is not null)
            {
                await _crudService.AddRecordAsync(createdSubdivision);
                AddItem(createdSubdivision);
            }
        }

        protected async override Task DeleteExecute()
        {
            if (SelectedSubdivision is null) return;

            await _crudService.RemoveRecordAsync(SelectedSubdivision);
            RemoveItem(SelectedSubdivision);
            SelectedSubdivision = null;
        }

        protected async override Task RefreshExecute()
        {
            if (SelectedSubdivision is null) return;

            var subdivisionViewModel = _subdivisionVmFactory();

            Subdivision? createdSubdivision = await WeakReferenceMessenger.Default.Send(new SubdivisionDialogMessage(subdivisionViewModel, SelectedSubdivision));

            if (createdSubdivision is not null)
            {
                await _crudService.UpdateRecordAsync(createdSubdivision);
                await LoadDataFromServer();
            }
        }

        protected override bool MatchesSearch(Subdivision subdivision, string searchText) =>
            subdivision.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }
}
