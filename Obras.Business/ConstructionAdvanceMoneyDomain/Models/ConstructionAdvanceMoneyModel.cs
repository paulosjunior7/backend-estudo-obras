using System;

namespace Obras.Business.ConstructionAdvanceMoneyDomain.Models
{
    public class ConstructionAdvanceMoneyModel
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
