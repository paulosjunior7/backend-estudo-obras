using System;

namespace Obras.Api.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Roles { get; set; }
        public Nullable<int> CompanyId { get; set; }
    }
}
