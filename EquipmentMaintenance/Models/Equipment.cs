using System;

namespace EquipmentMaintenance.Models
{
    public class Equipment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string InventoryNumber { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public required string Model { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public DateOnly DateOfCommissioning { get; set; }

        public override string ToString()
        {
            return $"{EquipmentType} {Model}".Trim();
        }
    }
}
