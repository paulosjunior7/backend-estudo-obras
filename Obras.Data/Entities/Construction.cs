using Obras.Data.Enums;
using System;
using System.Collections.Generic;

namespace Obras.Data.Entities
{
    public class Construction
    {
        #region Fields
        public int Id { get; set; }
        public string Identifier { get; set; }
        public StatusConstruction StatusConstruction { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
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
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string RegistrationUserId { get; set; }
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public bool Active { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<ConstructionAdvanceMoney> ConstructionAdvanceMoneys { get; set; }
        public ICollection<ConstructionBatch> ConstructionBatchs { get; set; }
        public ICollection<ConstructionDocumentation> ConstructionDocumentations { get; set; }
        public ICollection<ConstructionExpense> ConstructionExpenses { get; set; }
        public ICollection<ConstructionHouse> ConstructionHouses { get; set; }
        public ICollection<ConstructionInvestor> ConstructionInvestors { get; set; }
        public ICollection<ConstructionManpower> ConstructionManpowers { get; set; }
        public ICollection<ConstructionMaterial> ConstructionMaterials { get; set; }
        public ICollection<Photo> Photos { get; set; }

        #endregion
    }
}
