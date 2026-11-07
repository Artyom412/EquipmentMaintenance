using System;
using System.Collections.Generic;

namespace EquipmentMaintenance.Models
{
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }

        public List<Equipment> Equipments { get; set; } = [];
    }
}
