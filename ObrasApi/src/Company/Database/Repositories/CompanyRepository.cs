using ObrasApi.src.Company.Database.Domain;
using ObrasApi.src.Shared.Database;
using System;
using System.Linq;

namespace ObrasApi.src.Company.Database.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _db;

        public CompanyRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<CompanyDomain> GetAll()
        {
            return _db.Companies.AsQueryable();
        }

        public CompanyDomain GetById(Guid Id)
        {
            return _db.Companies.SingleOrDefault(t => t.Id == Id);
        }

        public CompanyDomain Save(CompanyDomain entity)
        {
            if (!entity.Id.HasValue)
            {
                entity.Id = Guid.NewGuid();
                _db.Companies.Add(entity);
            }
            _db.SaveChanges();
            return entity;
        }
    }
}