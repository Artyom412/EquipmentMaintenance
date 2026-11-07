using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class EmployeeObjectViewModel(ICrudService crudService) : ObjectBaseView
    {
        private readonly ICrudService _crudService = crudService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string? Name { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial JobTitle? SelectedJobTitle { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Subdivision? SelectedSubdivision { get; set; }

        public Employee? CreatedEmployee { get; private set; }

        public AvaloniaList<JobTitle> JobTitles { get; } = [];
        public AvaloniaList<Subdivision> Subdivisions { get; } = [];

        protected override bool CanExecute() =>
            !string.IsNullOrWhiteSpace(Name)
            && SelectedJobTitle is not null
            && SelectedSubdivision is not null;

        public async Task LoadDataFromServer()
        {
            var jobTitles = await _crudService.GetRecordsAsync<JobTitle>();
            var subdivisions = await _crudService.GetRecordsAsync<Subdivision>();

            JobTitles.AddRange(jobTitles);
            Subdivisions.AddRange(subdivisions);
        }

        protected override void CancelExecute()
        {
            CreatedEmployee = null;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }

        protected override void ConfirmExecute()
        {
            var employee = new Employee
            {
                Name = Name!,
                SubdivisionId = SelectedSubdivision!.Id,
                JobTitleId = SelectedJobTitle!.Id,
            };

            CreatedEmployee = employee;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }
    }
}
