using System;
namespace Obras.Data.Entities
{
    public class TotalExpense
    {
        public int Id { get; set; }
        public double? Material { get; set; }
        public double? Equipe { get; set; }
        public double? Documentacao { get; set; }
        public double? Despesa { get; set; }
        public double? ValorLote { get; set; }
        public double? ValorVenda { get; set; }
    }
}

