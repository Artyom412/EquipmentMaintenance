using Avalonia.Collections;
using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class AmountOfStatementsReportViewModel : ViewModelBase
    {
        private readonly ICrudService _crudService;

        public AvaloniaList<AmountOfStatementsTable> AmountOfStatements { get; } = [];
        public int TotalAmountOfStatements => AmountOfStatements.Sum(x => x.AmountOfStatements);

        public AmountOfStatementsReportViewModel(ICrudService crudService)
        {
            _crudService = crudService;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var table = await _crudService.GetAmountOfStatementsReport();
            AmountOfStatements.AddRange(table);
        }
    }
}
