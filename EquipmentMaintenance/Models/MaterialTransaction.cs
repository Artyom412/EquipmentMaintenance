using System;

namespace EquipmentMaintenance.Models
{
    public class MaterialTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateOnly Date { get; set; }
        public Guid? StatementId { get; set; }
        public Statement? Statement { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; } = null!;
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }

        public string TypeDescription => TransactionType switch
        {
            TransactionType.Income => "Приход",
            TransactionType.Expense => "Расход",
            _ => throw new NotImplementedException()
        };
    }
}
