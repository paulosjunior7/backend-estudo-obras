using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities 
{    
    public class ConstructionHouse
    {
        #region Fields
        public int Id { get; set; }
        public int ConstructionId { get; set; }
        public virtual Construction Construction { get; set; }
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
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        #endregion
    }
}
