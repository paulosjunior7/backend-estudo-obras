using System;

namespace Obras.Business.ConstructionMaterialDomain.Models
{
    public class ConstructionMaterialModel
    {
        public DateTime PurchaseDate { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int ConstructionId { get; set; }
        public int ProductId { get; set; }
        public int GroupId { get; set; }
        public int UnityId { get; set; }
        public int ProviderId { get; set; }
        public int BrandId { get; set; }
        public int ConstructionInvestorId { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
