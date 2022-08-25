using System;

namespace Obras.Business.ConstructionDocumentationDomain.Models
{
    public class  ConstructionDocumentationModel
    {
        public DateTime Date { get; set; }
        public int DocumentationId { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
