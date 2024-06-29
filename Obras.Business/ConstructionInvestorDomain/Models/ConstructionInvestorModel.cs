namespace Obras.Business.ConstructionInvestorDomain.Models
{
    public class  ConstructionInvestorModel
    {
        public int? Id { get; set; }
        public int ConstructionId { get; set; }
        public int PeopleId { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
