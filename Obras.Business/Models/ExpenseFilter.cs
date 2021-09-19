using Obras.Data.Enums;

namespace Obras.Business.Models
{
    public class ExpenseFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public TypeExpense? TypeExpense { get; set; }
        public int? CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
