using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class ConstructionManpower
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int OutsourcedId { get; set; }

        public virtual Outsourced Outsourced { get; set; }

        [Required]
        public double Value { get; set; }

        public int ConstructionId { get; set; }

        public virtual Construction Construction { get; set; }

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
