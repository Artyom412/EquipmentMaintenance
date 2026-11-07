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

        public async Task LoadDataFromServer()
        {
            var subdivisions = await _crudService.GetRecordsAsync<Subdivision>();
            Items.AddRange(subdivisions);
        }

        protected async override Task AddExecute()
        {
            var subdivisionViewModel = _subdivisionVmFactory();

            Subdivision? createdSubdivision = await WeakReferenceMessenger.Default.Send(new SubdivisionDialogMessage(subdivisionViewModel));

            if (createdSubdivision is not null)
            {
                await _crudService.AddRecordAsync(createdSubdivision);
                Items.Add(createdSubdivision);
            }
        }

        protected override void DeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override void RefreshExecute()
        {
            throw new NotImplementedException();
        }
    }
}
