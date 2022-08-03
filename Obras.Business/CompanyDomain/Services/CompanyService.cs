using Microsoft.EntityFrameworkCore;
using Obras.Business.CompanyDomain.Enums;
using Obras.Business.CompanyDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obras.Business.SharedDomain.Models;
using Obras.Business.SharedDomain.Enums;
using AutoMapper;

namespace Obras.Business.CompanyDomain.Services
{
    public interface ICompanyService
    {
        Task<Company> CreateAsync(CompanyModel company);
        Task<Company> UpdateCompanyAsync(int companyId, CompanyModel company);
        Task<PageResponse<Company>> GetCompaniesAsync(PageRequest<CompanyFilter, CompanySortingFields> pageRequest);
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
                var retorno =  await _dbContext.SaveChangesAsync();
            } catch (Exception e)
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

        public async Task<PageResponse<Company>> GetCompaniesAsync(PageRequest<CompanyFilter, CompanySortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Companies.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Company> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;
            

            #endregion

            return new PageResponse<Company>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Company> LoadOrder(PageRequest<CompanyFilter, CompanySortingFields> pageRequest, IQueryable<Company> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.Cnpj)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cnpj)
                    : dataQuery.OrderBy(x => x.Cnpj);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.CorporateName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.CorporateName)
                    : dataQuery.OrderBy(x => x.CorporateName);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.FantasyName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.FantasyName)
                    : dataQuery.OrderBy(x => x.FantasyName);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.Neighbourhood)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Neighbourhood)
                    : dataQuery.OrderBy(x => x.Neighbourhood);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.State)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.State)
                    : dataQuery.OrderBy(x => x.State);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.City)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.City)
                    : dataQuery.OrderBy(x => x.City);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Company> LoadFilterQuery(CompanyFilter filter, IQueryable<Company> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.CorporateName))
            {
                filterQuery = filterQuery.Where(x => x.CorporateName.ToLower().Contains(filter.CorporateName.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.FantasyName))
            {
                filterQuery = filterQuery.Where(x => x.FantasyName.ToLower().Contains(filter.FantasyName.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.Cnpj))
            {
                filterQuery = filterQuery.Where(x => x.Cnpj.Contains(filter.Cnpj));
            }
            if (!string.IsNullOrEmpty(filter.City))
            {
                filterQuery = filterQuery.Where(x => x.City.ToLower().Contains(filter.City.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.Neighbourhood))
            {
                filterQuery = filterQuery.Where(x => x.Neighbourhood.ToLower().Contains(filter.Neighbourhood.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.State))
            {
                filterQuery = filterQuery.Where(x => x.State.ToLower().Contains(filter.State.ToLower()));
            }
            if (filter.Active != null)
            {
                filterQuery = filterQuery.Where(x => x.Active == filter.Active);
            }

            return filterQuery;
        }
    }
}
