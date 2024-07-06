using Obras.Business.BrandDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Response;
using Obras.Business.GroupDomain.Models;
using Obras.Business.ProductDomain.Models;
using Obras.Business.ProviderDomain.Models;
using Obras.Business.UnitDomain.Models;
using System;
namespace Obras.Business.ConstructionMaterialDomain.Response
{
    public class ConstructionMaterialResponse
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }
        public int GroupId { get; set; }
        public virtual GroupModel Group { get; set; }
        public int UnityId { get; set; }
        public virtual UnityModel Unity { get; set; }
        public int ProviderId { get; set; }
        public virtual ProviderModel Provider { get; set; }
        public int BrandId { get; set; }
        public virtual BrandModel Brand { get; set; }
        public int ConstructionInvestorId { get; set; }
        public virtual ConstructionInvestorResponse ConstructionInvestor { get; set; }
        public bool Active { get; set; }
    }
}

