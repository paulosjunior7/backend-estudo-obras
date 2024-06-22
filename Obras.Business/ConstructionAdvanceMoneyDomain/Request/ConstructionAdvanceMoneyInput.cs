using System;
namespace Obras.Business.ConstructionAdvanceMoneyDomain.Request
{
    public class ConstructionAdvanceMoneyInput
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
    }
}

