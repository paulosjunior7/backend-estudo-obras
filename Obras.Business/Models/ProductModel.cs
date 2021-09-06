namespace Obras.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductModel
    {
        public string Description { get; set; }
        public string Detail { get; set; }
        public bool Active { get; set; }
        public int CompanyId { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
