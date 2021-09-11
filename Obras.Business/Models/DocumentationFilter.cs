namespace Obras.Business.Models
{
    public class DocumentationFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
