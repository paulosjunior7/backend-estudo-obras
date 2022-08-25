using System;

namespace Obras.Business.ConstructionManpowerDomain.Models
{
    public class ConstructionManpowerModel
    {
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public int OutsourcedId { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
