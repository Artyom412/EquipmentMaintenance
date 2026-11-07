using System;
using System.Collections.Generic;

namespace EquipmentMaintenance.Models
{
    public class Material
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string ItemNumber { get; set; }
        public required string Name { get; set; }

        public Guid UnitOfMeasurementId { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; } = null!;

        public int ConsumptionRate { get; set; }

        public List<Statement> Statements { get; set; } = [];
        public List<MaterialTransaction> MaterialTransactions { get; set; } = [];
    }
}
