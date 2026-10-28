using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class Statement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Arrival { get; set; }

        public Guid SubdivisionId { get; set; }
        public Subdivision Subdivision { get; set; } = null!;

        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;

        public required string Description { get; set; }
        public Priority Priority { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public DateTime? CompletionDate { get; set; }

        public List<Material> Materials { get; set; } = [];

        public string? Result { get; set; }
    }
}
