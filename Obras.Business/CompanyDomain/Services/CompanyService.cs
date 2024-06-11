using Microsoft.EntityFrameworkCore;
using Obras.Business.CompanyDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Obras.Business.CompanyDomain.Services
{
    public interface ICompanyService
    {
        Task<Company> CreateAsync(CompanyModel company);
        Task<Company> UpdateCompanyAsync(int companyId, CompanyModel company);
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyId(int id);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Company> CreateAsync(CompanyModel company)
        {
            var comp = _mapper.Map<Company>(company);
            comp.CreationDate = DateTime.Now;
            comp.ChangeDate = DateTime.Now;

            _dbContext.Companies.Add(comp);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return comp;
        }

        public async Task<Company> UpdateCompanyAsync(int companyId, CompanyModel company)
        {
            var comp = await _dbContext.Companies.FindAsync(companyId);

            if (comp != null)
            {
                comp.Active = company.Active;
                comp.Address = company.Address;
                comp.CellPhone = company.CellPhone;
                comp.ChangeDate = DateTime.Now;
                comp.EMail = company.EMail;
                comp.FantasyName = company.FantasyName;
                comp.Neighbourhood = company.Neighbourhood;
                comp.Number = company.Number;
                comp.State = company.State;
                comp.City = company.City;
                comp.Complement = company.Complement;
                comp.Telephone = company.Telephone;
                comp.ZipCode = company.ZipCode;
                await _dbContext.SaveChangesAsync();
            }

            return comp;
        }

        public async Task<Company> GetCompanyId(int id)
        {
            return await _dbContext.Companies.FindAsync(id);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync() =>
            await _dbContext.Companies.ToListAsync();

    }
}
