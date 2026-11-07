using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquipmentMaintenance.Messages;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class StatementObjectViewModel(ICrudService crudService) : ObjectBaseView
    {
        private readonly ICrudService _crudService = crudService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string Name { get; set; } = string.Empty;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial DateTimeOffset? Arrival { get; set; }
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Subdivision? SelectedSubdivision { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Equipment? SelectedEquipment { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Employee? SelectedEmployee { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial Material? SelectedMaterial { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string? Description { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial PriorityOption? SelectedPriority { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial DateTimeOffset? CompletionDate { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmExecuteCommand))]
        public partial string? Result { get; set; }

        public Statement? CreatedStatement { get; private set; }

        public AvaloniaList<Subdivision> Subdivisions { get; } = [];
        public AvaloniaList<Equipment> Equipments { get; } = [];
        public AvaloniaList<Employee> Employees { get; } = [];
        public AvaloniaList<Material> Materials { get; } = [];

        public PriorityOption[] PriorityOptions { get; } =
        [
            new PriorityOption(Priority.High, "Высокий"),
            new PriorityOption(Priority.Low, "Низкий")
        ];

        public async Task LoadDataFromServer()
        {
            var subdivisions = await _crudService.GetRecordsAsync<Subdivision>();
            Subdivisions.AddRange(subdivisions);

            var equipment = await _crudService.GetRecordsAsync<Equipment>();
            Equipments.AddRange(equipment);

            var employees = await _crudService.GetRecordsAsync<Employee>();
            Employees.AddRange(employees);

            var materials = await _crudService.GetRecordsAsync<Material>();
            Materials.AddRange(materials);
        }

        protected override bool CanExecute() =>
            !string.IsNullOrWhiteSpace(Name)
            && Arrival is not null
            && SelectedSubdivision is not null
            && SelectedMaterial is not null
            && SelectedEquipment is not null
            && SelectedEmployee is not null
            && CompletionDate is not null;


        protected override void CancelExecute()
        {
            CreatedStatement = null;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }

        protected override void ConfirmExecute()
        {
            var statement = new Statement
            {
                Name = Name,
                Arrival = Arrival!.Value.Date,
                SubdivisionId = SelectedSubdivision!.Id,
                EquipmentId = SelectedEquipment!.Id,
                Description = Description,
                Priority = SelectedPriority!.ValueType,
                EmployeeId = SelectedEmployee!.Id,
                CompletionDate = CompletionDate!.Value.Date,
                MaterialId = SelectedMaterial!.Id,
                Result = Result
            };

            CreatedStatement = statement;
            WeakReferenceMessenger.Default.Send(new CloseDialogMessage());
        }
    }

    public record PriorityOption(Priority ValueType, string Label);
}
