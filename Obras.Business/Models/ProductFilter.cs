namespace Obras.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductFilter
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public int? CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
