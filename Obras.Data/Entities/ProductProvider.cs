using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class ProductProvider
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AuxiliaryCode { get; set; }

        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public bool Active { get; set; }

        public string RegistrationUserId { get; set; }

        public virtual User RegistrationUser { get; set; }

        public string ChangeUserId { get; set; }

        public virtual User ChangeUser { get; set; }

        [Required]
        public DateTime? ChangeDate { get; set; }

        [Required]
        public DateTime? CreationDate { get; set; }

        #endregion
    }
}
