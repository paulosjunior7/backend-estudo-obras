using System;
namespace Obras.Business.ConstructionManpowerDomain.Request
{
    public class ConstructionManpowerInput
    {
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public int OutsourcedId { get; set; }
        public double Value { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
    }
}

