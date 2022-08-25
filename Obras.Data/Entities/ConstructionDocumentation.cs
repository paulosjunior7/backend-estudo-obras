namespace Obras.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class ConstructionDocumentation
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int DocumentationId { get; set; }

        public virtual Documentation Documentation { get; set; }

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
