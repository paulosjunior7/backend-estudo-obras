namespace Obras.Business.UnitDomain.Models
{
    public class UnityModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public double Multiplier { get; set; }
        public int? CompanyId { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
        public bool Active { get; set; }
    }
}
