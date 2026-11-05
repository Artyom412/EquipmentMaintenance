using EquipmentMaintenance.Models;
using EquipmentMaintenance.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public partial class StatementListViewModel(ICrudService crudService) : ListViewBase<Statement>
    {
        private readonly ICrudService _crudService = crudService;

        public async Task LoadDataFromServer()
        {
            var statements = await _crudService.GetRecordsAsync<Statement>();
            Items.AddRange(statements);
        }

        protected override Task AddExecute()
        {
            throw new NotImplementedException();
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
