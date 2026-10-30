using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }

        public Guid JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; } = null!;

        public Guid SubdivisionId { get; set; }
        public Subdivision Subdivision { get; set; } = null!;
    }
}
