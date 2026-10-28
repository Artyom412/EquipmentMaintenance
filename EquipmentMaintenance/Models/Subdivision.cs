using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class Subdivision
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Code { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
