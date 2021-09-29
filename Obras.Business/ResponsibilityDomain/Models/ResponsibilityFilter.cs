namespace Obras.Business.ResponsibilityDomain.Models
{
    public class ResponsibilityFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
