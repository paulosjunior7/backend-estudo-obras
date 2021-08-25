using ObrasApi.src.Account.Database.Domain;
using System;
using System.Linq;

namespace ObrasApi.src.Account.Database.Repositories
{
    public interface IAccountRepository
    {
        IQueryable<AccountDomain> GetAllUserCompany(Guid IdCompany);
        AccountDomain GetById(Guid Id);
        AccountDomain Save(AccountDomain entity);
    }
}