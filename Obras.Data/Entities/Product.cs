namespace Obras.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class Product
    {
        #region Fields
        public int Id { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
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
        public ICollection<ProductProvider> ProductProviders { get; set; }

        #endregion

        #region Ctor
        public Product()
        {

        }
        #endregion
    }
}
