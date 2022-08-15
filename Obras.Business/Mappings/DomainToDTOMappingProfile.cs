using AutoMapper;
using Obras.Business.BrandDomain.Models;
using Obras.Business.CompanyDomain.Models;
using Obras.Business.ConstructionBatchDomain.Models;
using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionHouseDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Models;
using Obras.Business.DocumentationDomain.Models;
using Obras.Business.EmployeeDomain.Models;
using Obras.Business.ExpenseDomain.Models;
using Obras.Business.OutsourcedDomain.Models;
using Obras.Business.PeopleDomain.Models;
using Obras.Business.ProductDomain.Models;
using Obras.Business.ProductProviderDomain.Models;
using Obras.Business.ResponsibilityDomain.Models;
using Obras.Business.UnitDomain.Models;
using Obras.Data.Entities;

namespace Obras.Business.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<BrandModel, Brand>().ReverseMap();
            CreateMap<CompanyModel, Company>().ReverseMap();
            CreateMap<DocumentationModel, Documentation>().ReverseMap();
            CreateMap<EmployeeModel, Employee>().ReverseMap();
            CreateMap<ExpenseModel, Expense>().ReverseMap();
            CreateMap<OutsourcedModel, Outsourced>().ReverseMap();
            CreateMap<PeopleModel, People>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<ProductProviderModel, ProductProvider>().ReverseMap();
            CreateMap<ResponsibilityModel, Responsibility>().ReverseMap();
            CreateMap<ConstructionModel, Construction>().ReverseMap();
            CreateMap<ConstructionInvestorModel, ConstructionInvestor>().ReverseMap();
            CreateMap<ConstructionBatchModel, ConstructionBatch>().ReverseMap();
            CreateMap<ConstructionHouseModel, ConstructionHouse>().ReverseMap();
            CreateMap<UnityModel, Unity>().ReverseMap();
        }
    }
}
