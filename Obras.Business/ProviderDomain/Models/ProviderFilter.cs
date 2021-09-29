namespace Obras.Business.ProviderDomain.Models
{

    public class ProviderFilter
    {
        public int? Id { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool? Active { get; set; }
        public int? CompanyId { get; set; }
    }
}
