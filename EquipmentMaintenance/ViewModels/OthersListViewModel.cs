using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;
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
