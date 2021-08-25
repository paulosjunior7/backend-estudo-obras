using ObrasApi.src.Account.Database.Domain;
using ObrasApi.src.Shared.Database;
using System;
using System.Linq;

namespace ObrasApi.src.Account.Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<AccountDomain> GetAllUserCompany(Guid idCompany)
        {
            return _db.Accounts.Where(t => t.IdCompany == idCompany).AsQueryable();
        }

        public AccountDomain GetById(Guid Id)
        {
            return _db.Accounts.SingleOrDefault(t => t.Id == Id);
        }

        public AccountDomain Save(AccountDomain entity)
        {
            if (!entity.Id.HasValue)
            {
                entity.Id = Guid.NewGuid();
                _db.Accounts.Add(entity);
            }
            _db.SaveChanges();
            return entity;
        }
    }
}