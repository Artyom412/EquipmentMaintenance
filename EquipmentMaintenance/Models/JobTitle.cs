using System;
using System.Collections.Generic;

namespace EquipmentMaintenance.Models
{
    public class JobTitle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }

        public List<Employee> Employees { get; set; } = [];
    }
}
