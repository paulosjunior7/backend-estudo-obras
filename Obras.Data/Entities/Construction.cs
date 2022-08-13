using Obras.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class Construction
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Identifier { get; set; }

        public StatusConstruction StatusConstruction { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Number { get; set; }

        [StringLength(100)]
        public string Neighbourhood { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(100)]
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

        [Required]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public string RegistrationUserId { get; set; }

        public virtual User RegistrationUser { get; set; }

        public string ChangeUserId { get; set; }

        public virtual User ChangeUser { get; set; }

        public bool Active { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        #endregion
    }
}
