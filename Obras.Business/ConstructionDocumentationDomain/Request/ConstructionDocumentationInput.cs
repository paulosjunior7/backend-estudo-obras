using System;
namespace Obras.Business.ConstructionDocumentationDomain.Request
{
    public class ConstructionDocumentationInput
    {
        public DateTime Date { get; set; }
        public int DocumentationId { get; set; }
        public double Value { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
    }
}

