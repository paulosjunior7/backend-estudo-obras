using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class ConstructionMaterial
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        public int ConstructionId { get; set; }

        public virtual Construction Construction { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int UnityId { get; set; }

        public virtual Unity Unity { get; set; }

        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public int ConstructionInvestorId { get; set; }

        public virtual ConstructionInvestor ConstructionInvestor { get; set; }

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
