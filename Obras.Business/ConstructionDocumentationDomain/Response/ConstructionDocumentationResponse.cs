using Obras.Business.DocumentationDomain.Models;
using Obras.Data.Entities;
using System;
namespace Obras.Business.ConstructionDocumentationDomain.Response
{
    public class ConstructionDocumentationResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DocumentationId { get; set; }
        public virtual DocumentationModel Documentation { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public int ConstructionInvestorId { get; set; }
        public virtual ConstructionInvestor ConstructionInvestor { get; set; }
        public bool Active { get; set; }
    }
}

