using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class ExpenseOfMaterial
    {
        public required string MaterialName { get; set; }
        public int Expense { get; set; }
    }
}
