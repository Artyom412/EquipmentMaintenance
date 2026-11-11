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
            await RunSafelyAsync(async () =>
            {
                var jobTitles = await _crudService.GetRecordsAsync<JobTitle>();
                var locations = await _crudService.GetRecordsAsync<Location>();
                var measurements = await _crudService.GetRecordsAsync<UnitOfMeasurement>();

                JobTitles.Clear();
                Locations.Clear();
                Measurements.Clear();
                JobTitles.AddRange(jobTitles);
                Locations.AddRange(locations);
                Measurements.AddRange(measurements);
            });
        }

        private bool CanExecute() => !string.IsNullOrWhiteSpace(Name);

        [RelayCommand]
        private async Task RemoveJobTitle()
        {
            await RunSafelyAsync(async () =>
            {
                if (SelectedJobTitle is null) return;

                await _crudService.RemoveRecordAsync(SelectedJobTitle);
                JobTitles.Remove(SelectedJobTitle);
                SelectedJobTitle = null;
            });
        }

        [RelayCommand]
        private async Task RemoveLocation()
        {
            await RunSafelyAsync(async () =>
            {
                if (SelectedLocation is null) return;

                await _crudService.RemoveRecordAsync(SelectedLocation);
                Locations.Remove(SelectedLocation);
                SelectedLocation = null;
            });
        }

        [RelayCommand]
        private async Task RemoveMeasurement()
        {
            await RunSafelyAsync(async () =>
            {
                if (SelectedMeasurement is null) return;

                await _crudService.RemoveRecordAsync(SelectedMeasurement);
                Measurements.Remove(SelectedMeasurement);
                SelectedMeasurement = null;
            });
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task AddRecord(string parametr)
        {
            await RunSafelyAsync(async () =>
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
            });
        }

        [RelayCommand]
        private async Task UpdateJobTitle()
        {
            await RunSafelyAsync(async () =>
            {
                if (SelectedJobTitle is null) return;

                var jobTitleCopy = new JobTitle { Id = SelectedJobTitle.Id, Name = Name };
                await _crudService.UpdateRecordAsync(jobTitleCopy);
                JobTitles.Clear();
                var jobTitles = await _crudService.GetRecordsAsync<JobTitle>();
                JobTitles.AddRange(jobTitles);
            });
        }

        [RelayCommand]
        private async Task UpdateLocation()
        {
            await RunSafelyAsync(async () =>
            {
                if (SelectedLocation is null) return;

                var locationCopy = new Location { Id = SelectedLocation.Id, Name = Name };
                await _crudService.UpdateRecordAsync(locationCopy);
                Locations.Clear();
                var locations = await _crudService.GetRecordsAsync<Location>();
                Locations.AddRange(locations);
            });
        }

        [RelayCommand]
        private async Task UpdateMeasurements()
        {
            await RunSafelyAsync(async () =>
            {
                if (SelectedMeasurement is null) return;

                var measurementCopy = new UnitOfMeasurement { Id = SelectedMeasurement.Id, Name = Name };
                await _crudService.UpdateRecordAsync(measurementCopy);
                Measurements.Clear();
                var measurements = await _crudService.GetRecordsAsync<UnitOfMeasurement>();
                Measurements.AddRange(measurements);
            });
        }
    }
}
