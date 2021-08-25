using ObrasApi.src.Company.Database.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasApi.src.Account.Database.Domain
{
    public class AccountDomain
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("companies")]
        public Guid IdCompany { get; set; }

        public virtual CompanyDomain Company { get; set; }

        public bool Active { get; set; }

        public DateTime? ChangeDate { get; set; }

        public DateTime? CreationDate { get; set; }

    }
}