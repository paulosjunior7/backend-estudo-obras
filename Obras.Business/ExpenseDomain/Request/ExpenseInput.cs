using Obras.Data.Enums;
using System;
namespace Obras.Business.ExpenseDomain.Request
{
    public class ExpenseInput
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public TypeExpense TypeExpense { get; set; }
    }
}

