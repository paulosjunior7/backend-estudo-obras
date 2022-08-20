namespace Obras.Business.GroupDomain.Models
{
    public class GroupFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
