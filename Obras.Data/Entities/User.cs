namespace Obras.Data.Entities
{
    public class User: Microsoft.AspNetCore.Identity.IdentityUser
    { 
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }        
    }
}
