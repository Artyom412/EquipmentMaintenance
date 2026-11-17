using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class AmountOfStatementsTable
    {
        public required string NameOfResponsiblePerson { get; set; }
        public int AmountOfStatements { get; set; }
    }
}
