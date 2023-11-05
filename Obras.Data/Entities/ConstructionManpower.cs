using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class ConstructionManpower
    {
        #region Fields
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int OutsourcedId { get; set; }
        public virtual Outsourced Outsourced { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public virtual Construction Construction { get; set; }
        public int ConstructionInvestorId { get; set; }
        public virtual ConstructionInvestor ConstructionInvestor { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        #endregion

    }
}
