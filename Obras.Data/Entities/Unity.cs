using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class Unity
    {
        #region Fields
        public int Id { get; set; }
        public string Description { get; set; }
        public double Multiplier { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string RegistrationUserId { get; set; }
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public bool Active { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        public ICollection<ConstructionMaterial> ConstructionMaterials { get; set; }

        #endregion
    }
}
