using Microsoft.EntityFrameworkCore;
using Obras.Business.Enums;
using Obras.Business.Helpers;
using Obras.Business.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obras.Business.Services
{
    public interface ICompanyService
    {
        Task<Company> CreateAsync(CompanyModel company);
        Task<Company> UpdateStatusAsync(int companyId, CompanyModel company);
        Task<PageResponse<Company>> GetCompaniesAsync(PageRequest<CompanyFilter, CompanySortingFields> pageRequest);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ObrasDBContext _dbContext;

        public CompanyService(ObrasDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Company> CreateAsync(CompanyModel company)
        {
            var comp = new Company
            {
                Cnpj = company.Cnpj,
                CreationDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                Active = company.Active,
                Address = company.Address,
                CellPhone = company.CellPhone,
                EMail = company.EMail,
                CorporateName = company.CorporateName,
                FantasyName = company.FantasyName,
                Neighbourhood = company.Neighbourhood,
                Number = company.Number,
                State = company.State,
                Telephone = company.Telephone,
                ZipCode = company.ZipCode
            };

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

        public async Task<Company> UpdateStatusAsync(int companyId, CompanyModel company)
        {
            var comp = await _dbContext.Companies.FindAsync(company);

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
                comp.Telephone = company.Telephone;
                comp.ZipCode = company.ZipCode;
                await _dbContext.SaveChangesAsync();
            }

            return comp;
        }

        public async Task<PageResponse<Company>> GetCompaniesAsync(PageRequest<CompanyFilter, CompanySortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Companies.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            if (pageRequest.First.HasValue)
            {
                if (!string.IsNullOrEmpty(pageRequest.After))
                {
                    int lastId = CursorHelper.FromCursor(pageRequest.After);
                    dataQuery = dataQuery.Where(x => x.Id > lastId);
                }

                dataQuery = dataQuery.Take(pageRequest.First.Value);
            }

            dataQuery = LoadOrder(pageRequest, dataQuery);

            List<Company> nodes = await dataQuery.ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = await filterQuery.AnyAsync(x => x.Id > maxId);
            bool hasPrevPage = await filterQuery.AnyAsync(x => x.Id < minId);
            int totalCount = await filterQuery.CountAsync();

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
                dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cnpj)
                    : dataQuery.OrderBy(x => x.Cnpj);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.CorporateName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.CorporateName)
                    : dataQuery.OrderBy(x => x.CorporateName);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.FantasyName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.FantasyName)
                    : dataQuery.OrderBy(x => x.FantasyName);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.Neighbourhood)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Neighbourhood)
                    : dataQuery.OrderBy(x => x.Neighbourhood);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.State)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.State)
                    : dataQuery.OrderBy(x => x.State);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.City)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.City)
                    : dataQuery.OrderBy(x => x.City);
            }
            else if (pageRequest.OrderBy?.Field == Enums.CompanySortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
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
