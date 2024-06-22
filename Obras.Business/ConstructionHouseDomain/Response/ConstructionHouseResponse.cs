using System;
namespace Obras.Business.ConstructionHouseDomain.Response
{
    public class ConstructionHouseResponse
    {
        public int Id { get; set; }
        public int ConstructionId { get; set; }
        public string Description { get; set; }
        public double? FractionBatch { get; set; }
        public double? BuildingArea { get; set; }
        public double? PermeableArea { get; set; }
        public string Registration { get; set; }
        public string EnergyConsumptionUnit { get; set; }
        public string WaterConsumptionUnit { get; set; }
        public double? SaleValue { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
    }
}

