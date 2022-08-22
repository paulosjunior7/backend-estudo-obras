using System;

namespace Obras.Business.ConstructionMaterialDomain.Models
{
    public class ConstructionMaterialFilter
    {
        public DateTime? PurchaseDate { get; set; }
        public double? Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public int? ConstructionId { get; set; }
        public int? ProductId { get; set; }
        public int? GroupId { get; set; }
    }
}
