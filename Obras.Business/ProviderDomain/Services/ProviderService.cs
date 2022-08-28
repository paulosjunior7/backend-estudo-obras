﻿namespace Obras.Business.ProviderDomain.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Obras.Business.ProviderDomain.Enums;
    using Obras.Business.ProviderDomain.Models;
    using Obras.Business.SharedDomain.Models;
    using Obras.Data;
    using Obras.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IProviderService
    {
        Task<Provider> CreateAsync(ProviderModel provider);
        Task<Provider> UpdateProviderAsync(int providerId, ProviderModel provider);
        Task<PageResponse<Provider>> GetProvidersAsync(PageRequest<ProviderFilter, ProviderSortingFields> pageRequest);
        Task<Provider> GetProviderId(int id);
    }

    public class ProviderService : IProviderService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ProviderService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Provider> CreateAsync(ProviderModel provider)
        {
            var prov = _mapper.Map<Provider>(provider);
            prov.CreationDate = DateTime.Now;
            prov.ChangeDate = DateTime.Now;
            prov.CompanyId = (int)(provider.CompanyId == null ? 0 : provider.CompanyId);

            _dbContext.Providers.Add(prov);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return prov;
        }

        public async Task<Provider> UpdateProviderAsync(int providerId, ProviderModel provider)
        {
            var prov = await _dbContext.Providers.FindAsync(providerId);

            if (prov != null)
            {
                prov.Active = provider.Active;
                prov.Cnpj = provider.Cnpj;
                prov.Address = provider.Address;
                prov.CellPhone = provider.CellPhone;
                prov.ChangeDate = DateTime.Now;
                prov.EMail = provider.EMail;
                prov.Name = provider.Name;
                prov.Neighbourhood = provider.Neighbourhood;
                prov.City = provider.City;
                prov.Complement = provider.Complement;
                prov.Number = provider.Number;
                prov.State = provider.State;
                prov.Telephone = provider.Telephone;
                prov.ZipCode = provider.ZipCode;
                prov.ChangeUserId = provider.ChangeUserId;
                prov.CompanyId = (int)(provider.CompanyId == null ? 0 : provider.CompanyId);
                await _dbContext.SaveChangesAsync();
            }

            return prov;
        }

        public async Task<Provider> GetProviderId(int id)
        {
            return await _dbContext.Providers.FindAsync(id);
        }

        public async Task<PageResponse<Provider>> GetProvidersAsync(PageRequest<ProviderFilter, ProviderSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Providers.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Provider> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Provider>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Provider> LoadOrder(PageRequest<ProviderFilter, ProviderSortingFields> pageRequest, IQueryable<Provider> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ProviderSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProviderSortingFields.Cnpj)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cnpj)
                    : dataQuery.OrderBy(x => x.Cnpj);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProviderSortingFields.Name)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Name)
                    : dataQuery.OrderBy(x => x.Name);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProviderSortingFields.Neighbourhood)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Neighbourhood)
                    : dataQuery.OrderBy(x => x.Neighbourhood);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProviderSortingFields.State)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.State)
                    : dataQuery.OrderBy(x => x.State);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProviderSortingFields.City)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.City)
                    : dataQuery.OrderBy(x => x.City);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProviderSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Provider> LoadFilterQuery(ProviderFilter filter, IQueryable<Provider> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                filterQuery = filterQuery.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
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
