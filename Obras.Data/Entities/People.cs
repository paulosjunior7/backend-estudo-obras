using Obras.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class People
    {
        #region Fields
        public int Id { get; set; }
        public TypePeople TypePeople { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
        public bool Constructor { get; set; }
        public bool Investor { get; set; }
        public bool Client { get; set; }
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
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string RegistrationUserId { get; set; }
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        public ICollection<ConstructionBatch> ConstructionBatchs { get; set; }
        public ICollection<ConstructionInvestor> ConstructionInvestors { get; set; }

        #endregion
    }
}
