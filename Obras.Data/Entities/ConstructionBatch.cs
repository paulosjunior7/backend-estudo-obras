using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class ConstructionBatch
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ConstructionId { get; set; }

        public virtual Construction Construction { get; set; }

        public int PeopleId { get; set; }

        public virtual People People { get; set; }

        public double Value { get; set; }

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
