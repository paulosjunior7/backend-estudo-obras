using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class Unity
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public double Multiplier { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public string RegistrationUserId { get; set; }

        public virtual User RegistrationUser { get; set; }

        public string ChangeUserId { get; set; }

        public virtual User ChangeUser { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime? ChangeDate { get; set; }

        [Required]
        public DateTime? CreationDate { get; set; }

        public ICollection<ConstructionMaterial> ConstructionMaterials { get; set; }

        #endregion
    }
}
