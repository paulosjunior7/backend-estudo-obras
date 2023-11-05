using System;
using System.Collections.Generic;

namespace Obras.Data.Entities
{
    public class User: Microsoft.AspNetCore.Identity.IdentityUser
    { 
        public Nullable<int> CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public ICollection<Brand> RegistrationBrands { get; set; }
        public ICollection<Brand> ChangeBrands { get; set; }
        public ICollection<Construction> RegistrationConstructions { get; set; }
        public ICollection<Construction> ChangeConstructions { get; set; }
        public ICollection<ConstructionAdvanceMoney> RegistrationConstructionAdvanceMoneys { get; set; }
        public ICollection<ConstructionAdvanceMoney> ChangeConstructionAdvanceMoneys { get; set; }
        public ICollection<ConstructionBatch> RegistrationConstructionBatchs { get; set; }
        public ICollection<ConstructionBatch> ChangeConstructionBatchs { get; set; }
        public ICollection<ConstructionDocumentation> RegistrationConstructionDocumentations { get; set; }
        public ICollection<ConstructionDocumentation> ChangeConstructionDocumentations { get; set; }
        public ICollection<ConstructionExpense> RegistrationConstructionExpenses { get; set; }
        public ICollection<ConstructionExpense> ChangeConstructionExpenses { get; set; }
        public ICollection<ConstructionHouse> RegistrationConstructionHouses { get; set; }
        public ICollection<ConstructionHouse> ChangeConstructionHouses { get; set; }
        public ICollection<ConstructionInvestor> RegistrationConstructionInvestors { get; set; }
        public ICollection<ConstructionInvestor> ChangeConstructionInvestors { get; set; }
        public ICollection<ConstructionManpower> RegistrationConstructionManpowers { get; set; }
        public ICollection<ConstructionManpower> ChangeConstructionManpowers { get; set; }
        public ICollection<ConstructionMaterial> RegistrationConstructionMaterials { get; set; }
        public ICollection<ConstructionMaterial> ChangeConstructionMaterials { get; set; }
        public ICollection<Documentation> RegistrationDocumentations { get; set; }
        public ICollection<Documentation> ChangeDocumentations { get; set; }
        public ICollection<Employee> RegistrationEmployees { get; set; }
        public ICollection<Employee> ChangeEmployees { get; set; }
        public ICollection<Expense> RegistrationExpenses { get; set; }
        public ICollection<Expense> ChangeExpenses { get; set; }
        public ICollection<Group> RegistrationGroups { get; set; }
        public ICollection<Group> ChangeGroups { get; set; }
        public ICollection<Outsourced> RegistrationOutsourceds { get; set; }
        public ICollection<Outsourced> ChangeOutsourceds { get; set; }
        public ICollection<People> RegistrationPeoples { get; set; }
        public ICollection<People> ChangePeoples { get; set; }
        public ICollection<Photo> RegistrationPhotos { get; set; }
        public ICollection<Photo> ChangePhotos { get; set; }
    }
}
