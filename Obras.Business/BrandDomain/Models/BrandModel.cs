namespace Obras.Business.BrandDomain.Models
{
    public class BrandModel
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public int? CompanyId { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
