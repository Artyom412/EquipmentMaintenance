using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Models
{
    public class MaterialTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateOnly Date { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; } = null!;
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
    }
}
