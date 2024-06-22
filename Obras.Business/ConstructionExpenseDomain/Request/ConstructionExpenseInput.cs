using System;
namespace Obras.Business.ConstructionExpenseDomain.Request
{
    public class ConstructionExpenseInput
    {
        public DateTime Date { get; set; }
        public int ExpenseId { get; set; }
        public double Value { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
    }
}

