namespace Obras.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompanyFilter
    {
        public int? Id { get; set; }
        public string Cnpj { get; set; }
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool? Active { get; set; }
    }
}
