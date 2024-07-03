
using System;
namespace Obras.Business.BrandDomain.Response
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}

