using Obras.Data.Enums;
using System;
namespace Obras.Business.ConstructionDomain.Request
{
    public class ConstructionInput
    {
        public string? Identifier { get; set; }
        public StatusConstruction? StatusConstruction { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? Number { get; set; }
        public string? Neighbourhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Complement { get; set; }
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
        public bool? Active { get; set; }
    }
}

