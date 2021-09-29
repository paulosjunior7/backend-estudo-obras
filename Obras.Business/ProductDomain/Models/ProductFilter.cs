namespace Obras.Business.ProductDomain.Models
{
    public class ProductFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public int? CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
