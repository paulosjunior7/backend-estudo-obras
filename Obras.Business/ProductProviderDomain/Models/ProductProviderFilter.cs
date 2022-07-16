namespace Obras.Business.ProductProviderDomain.Models
{
    public  class ProductProviderFilter
    {
        public int? Id { get; set; }
        public string AuxiliaryCode { get; set; }
        public int? ProductId { get; set; }
        public int? ProviderId { get; set; }
        public bool? Active { get; set; }
    }
}
