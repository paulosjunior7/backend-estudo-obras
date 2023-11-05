namespace Obras.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class Provider
    {
        #region Fields
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public string Telephone { get; set; }
        public string CellPhone { get; set; }
        public string EMail { get; set; }
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

    }
}
