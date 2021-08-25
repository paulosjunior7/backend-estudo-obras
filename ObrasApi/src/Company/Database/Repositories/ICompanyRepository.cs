using ObrasApi.src.Company.Database.Domain;
using System;
using System.Linq;

namespace ObrasApi.src.Company.Database.Repositories
{
    public interface ICompanyRepository
    {
        IQueryable<CompanyDomain> GetAll();
        CompanyDomain GetById(Guid Id);
        CompanyDomain Save(CompanyDomain entity);
    }
}