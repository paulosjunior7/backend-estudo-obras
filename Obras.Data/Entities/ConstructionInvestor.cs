using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class ConstructionInvestor
    {
        #region Fields
        public int Id { get; set; }
        public int ConstructionId { get; set; }
        public virtual Construction Construction { get; set; }
        public int PeopleId { get; set; }
        public virtual People People { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

        public ICollection<ConstructionAdvanceMoney> ConstructionAdvanceMoneys { get; set; }
        public ICollection<ConstructionDocumentation> ConstructionDocumentations { get; set; }
        public ICollection<ConstructionExpense> ConstructionExpenses { get; set; }
        public ICollection<ConstructionMaterial> ConstructionMaterials { get; set; }

        #endregion
    }
}
