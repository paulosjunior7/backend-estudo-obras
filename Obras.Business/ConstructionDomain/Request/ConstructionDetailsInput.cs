using Obras.Data.Enums;
using System;
namespace Obras.Business.ConstructionDomain.Request
{
    public class ConstructionDetailsInput
    {
        public int? BatchArea { get; set; }
        public int? BuildingArea { get; set; }
        public int? MunicipalRegistration { get; set; }
        public int? License { get; set; }
        public int? UndergroundUse { get; set; }
        public int? Art { get; set; }
        public int? Cno { get; set; }
        public double? MotherEnrollment { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? SaleValue { get; set; }
    }
}

