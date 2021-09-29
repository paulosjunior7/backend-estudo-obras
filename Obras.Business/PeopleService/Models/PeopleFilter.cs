using Obras.Data.Enums;

namespace Obras.Business.PeopleDomain.Models
{
    public class PeopleFilter
    {
        public int? Id { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public TypePeople? TypePeople { get; set; }
        public bool Constructor { get; set; }
        public bool Investor { get; set; }
        public bool Client { get; set; }
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool? Active { get; set; }
        public int? CompanyId { get; set; }
    }
}
