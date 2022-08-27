using System;

namespace Obras.Business.ConstructionExpenseDomain.Models
{
    public class ConstructionExpenseModel
    {
        public DateTime Date { get; set; }
        public int ExpenseId { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
