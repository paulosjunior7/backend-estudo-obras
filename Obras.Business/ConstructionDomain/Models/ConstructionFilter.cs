using Obras.Data.Enums;
using System;

namespace Obras.Business.ConstructionDomain.Models
{
    public class ConstructionFilter
    {
        public int? Id { get; set; }
        public string Identifier { get; set; }
        public StatusConstruction? StatusConstruction { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool? Active { get; set; }

        public int? CompanyId { get; set; }
    }
}
