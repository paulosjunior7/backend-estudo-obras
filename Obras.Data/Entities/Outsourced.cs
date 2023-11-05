namespace Obras.Data.Entities
{
    using Obras.Data.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class Outsourced
    {
        #region Fields
        public int Id { get; set; }
        public TypePeople TypePeople { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
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
        public bool Active { get; set; }
        public int ResponsibilityId { get; set; }
        public virtual Responsibility Responsibility { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string RegistrationUserId { get; set; }
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        public ICollection<ConstructionManpower> ConstructionManpowers { get; set; }

        #endregion
    }
}
