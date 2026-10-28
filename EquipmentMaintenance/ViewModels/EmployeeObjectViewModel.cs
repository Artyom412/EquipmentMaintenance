using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        public partial JobTitle? SelectedJobTitle { get; set; }

        public Employee? CreatedEmployee { get; private set; }

        public AvaloniaList<JobTitle> JobTitles { get; } = [];

        protected override bool CanExecute() =>
            !string.IsNullOrWhiteSpace(Name)
            && SelectedJobTitle is not null;

        public async Task LoadDataFromServer()
        {
            var jobTitles = await _crudService.GetRecordsAsync<JobTitle>();
            JobTitles.AddRange(jobTitles);
        }

        protected override void CancelExecute()
        {
            CreatedEmployee = null;
        }

        protected override void ConfirmExecute()
        {
            var employee = new Employee
            {
                Name = Name!,
                JobTitleId = SelectedJobTitle!.Id,
            };

            CreatedEmployee = employee;
        }
    }
}
