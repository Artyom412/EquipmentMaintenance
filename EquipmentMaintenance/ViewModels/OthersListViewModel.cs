using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class OthersListViewModel(ICrudService crudService) : ViewModelBase
    {
        private readonly ICrudService _crudService = crudService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddRecordCommand))]
        public partial string Name { get; set; } = string.Empty;

        public AvaloniaList<JobTitle> JobTitles { get; } = [];
        public AvaloniaList<Location> Locations { get; } = [];
        public AvaloniaList<UnitOfMeasurement> Measurements { get; } = [];

        public JobTitle? SelectedJobTitle { get; set; }
        public Location? SelectedLocation { get; set; }
        public UnitOfMeasurement? SelectedMeasurement { get; set; }

        public async Task LoadDataFromServer()
        {
            var jobTitles = await _crudService.GetRecordsAsync<JobTitle>();
            var locations = await _crudService.GetRecordsAsync<Location>();
            var measurements = await _crudService.GetRecordsAsync<UnitOfMeasurement>();

            JobTitles.AddRange(jobTitles);
            Locations.AddRange(locations);
            Measurements.AddRange(measurements);
        }

        private bool CanExecute() => !string.IsNullOrWhiteSpace(Name);

        [RelayCommand]
        private async Task RemoveJobTitle()
        {
            if (SelectedJobTitle is null) return;

            await _crudService.RemoveRecordAsync(SelectedJobTitle);
            JobTitles.Remove(SelectedJobTitle);
            SelectedJobTitle = null;
        }

        [RelayCommand]
        private async Task RemoveLocation()
        {
            if (SelectedLocation is null) return;

            await _crudService.RemoveRecordAsync(SelectedLocation);
            Locations.Remove(SelectedLocation);
            SelectedLocation = null;
        }

        [RelayCommand]
        private async Task RemoveMeasurement()
        {
            if (SelectedMeasurement is null) return;

            await _crudService.RemoveRecordAsync(SelectedMeasurement);
            Measurements.Remove(SelectedMeasurement);
            SelectedMeasurement = null;
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task AddRecord(string parametr)
        {
            Location CreateLocation() => new() { Name = Name };

            JobTitle CreateJobTitle() => new() { Name = Name };

            UnitOfMeasurement CreateUnitOfMeasure() => new() { Name = Name };

            switch (parametr)
            {
                case "Location":
                    Location location = CreateLocation();
                    await _crudService.AddRecordAsync(location);
                    Locations.Add(location); break;
                case "JobTitle":
                    JobTitle jobTitle = CreateJobTitle();
                    await _crudService.AddRecordAsync(jobTitle);
                    JobTitles.Add(jobTitle); break;
                case "Measure":
                    UnitOfMeasurement measure = CreateUnitOfMeasure();
                    await _crudService.AddRecordAsync(measure);
                    Measurements.Add(measure); break;
            }
        }
    }
}
