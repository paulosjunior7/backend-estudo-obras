using ObrasApi.src.Account.Database.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ObrasApi.src.Company.Database.Domain
{
    public class CompanyDomain
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public string Cnpj { get; set; }

        [Required]
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
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual ICollection<AccountDomain> Accounts { get; set; }
    }
}