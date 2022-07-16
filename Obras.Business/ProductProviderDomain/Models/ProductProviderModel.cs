namespace Obras.Business.ProductProviderDomain.Models
{
    public class ProductProviderModel
    {
        public string AuxiliaryCode { get; set; }
        public int ProductId { get; set; }
        public int ProviderId { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
        public bool Active { get; set; }
    }
}
