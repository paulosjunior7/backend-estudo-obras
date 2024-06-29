using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Response;
using Obras.Business.EmployeeDomain.Models;
using Obras.Business.OutsourcedDomain.Models;
using Obras.Data.Entities;
using System;
namespace Obras.Business.ConstructionManpowerDomain.Response
{
    public class ConstructionManpowerResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public int OutsourcedId { get; set; }
        public virtual OutsourcedModel Outsourced { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public virtual ConstructionModel Construction { get; set; }
        public int ConstructionInvestorId { get; set; }
        public virtual ConstructionInvestorResponse ConstructionInvestor { get; set; }
        public bool Active { get; set; }
    }
}

