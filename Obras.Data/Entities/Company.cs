using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class Company
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(18)]
        public string Cnpj { get; set; }

        [Required]
        [StringLength(100)]
        public string CorporateName { get; set; }

        [StringLength(100)]
        public string FantasyName { get; set; }

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

        [StringLength(18)]
        public string Telephone { get; set; }

        [StringLength(18)]
        public string CellPhone { get; set; }

        [StringLength(100)]
        public string EMail { get; set; }
        
        public bool Active { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        #endregion

        #region Ctor
        public Company()
        {

        }
        #endregion
    }
}