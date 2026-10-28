using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class OthersListViewModel(ICrudService crudService) : ObservableObject
    {
        private readonly ICrudService _crudService = crudService;

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


    }
}
