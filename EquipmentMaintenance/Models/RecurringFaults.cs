using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class RecurringFaults
    {
        public required string FaultName { get; set; }
        public int Quantity { get; set; }
    }
}
