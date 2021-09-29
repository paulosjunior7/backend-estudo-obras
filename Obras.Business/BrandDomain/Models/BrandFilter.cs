namespace Obras.Business.BrandDomain.Models
{
    public class BrandFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
