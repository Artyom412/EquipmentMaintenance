using System;
using System.Collections.Generic;

namespace EquipmentMaintenance.Models
{
    public class Statement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public DateTime Arrival { get; set; }

        public Guid SubdivisionId { get; set; }
        public Subdivision Subdivision { get; set; } = null!;

        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;

        public required string? Description { get; set; }
        public Priority Priority { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public DateTime? CompletionDate { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        public int Quantity { get; set; }

        public string? Result { get; set; }

        public string PriorityDescription => Priority switch
        {
            Priority.High => "Высокий",
            Priority.Low => "Низкий",
            _ => throw new NotImplementedException()
        };
    }
}
