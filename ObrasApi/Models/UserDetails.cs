using Obras.Business.CompanyDomain.Models;
namespace Obras.Api.Models
{
    public class UserDetails
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
        public CompanyModel? Company { get; set; }
    }
}

