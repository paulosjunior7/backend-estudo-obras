using Microsoft.EntityFrameworkCore;
using Obras.Business.ProductProviderDomain.Enums;
using Obras.Business.ProductProviderDomain.Models;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ProductProviderDomain.Services
{
    public interface IProductProviderService
    {
        Task<ProductProvider> CreateAsync(ProductProviderModel productProvider);
        Task<ProductProvider> UpdateProductProviderAsync(int productProviderId, ProductProviderModel productProvider);
        Task<PageResponse<ProductProvider>> GetProductProvidersAsync(PageRequest<ProductProviderFilter, ProductProviderSortingFields> pageRequest);
        Task<ProductProvider> GetProductProviderId(int id);
    }

    public class ProductProviderService : IProductProviderService
    {
        private readonly ObrasDBContext _dbContext;

        public ProductProviderService(ObrasDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductProvider> CreateAsync(ProductProviderModel productProvider)
        {
            var prod = new ProductProvider
            {
                AuxiliaryCode = productProvider.AuxiliaryCode,
                CreationDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                Active = productProvider.Active,
                ProviderId = productProvider.ProviderId,
                ProductId = productProvider.ProductId,
                ChangeUserId = productProvider.ChangeUserId,
                RegistrationUserId = productProvider.RegistrationUserId
            };

            _dbContext.ProductProviders.Add(prod);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return prod;
        }

        public async Task<ProductProvider> UpdateProductProviderAsync(int productProviderId, ProductProviderModel productProvider)
        {
            var prov = await _dbContext.ProductProviders.FindAsync(productProviderId);

            if (prov != null)
            {
                prov.Active = productProvider.Active;
                prov.ProductId = productProvider.ProductId;
                prov.ProviderId = productProvider.ProviderId;
                prov.ChangeDate = DateTime.Now;
                prov.AuxiliaryCode = productProvider.AuxiliaryCode;
                prov.ChangeUserId = productProvider.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return prov;
        }

        public async Task<ProductProvider> GetProductProviderId(int id)
        {
            return await _dbContext.ProductProviders.FindAsync(id);
        }

        public async Task<PageResponse<ProductProvider>> GetProductProvidersAsync(PageRequest<ProductProviderFilter, ProductProviderSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ProductProviders.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            int totalCount = await dataQuery.CountAsync();

            List<ProductProvider> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ProductProvider>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ProductProvider> LoadOrder(PageRequest<ProductProviderFilter, ProductProviderSortingFields> pageRequest, IQueryable<ProductProvider> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ProductProviderSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProductProviderSortingFields.AuxiliaryCode)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.AuxiliaryCode)
                    : dataQuery.OrderBy(x => x.AuxiliaryCode);
            }

            return dataQuery;
        }

        private static IQueryable<ProductProvider> LoadFilterQuery(ProductProviderFilter filter, IQueryable<ProductProvider> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (filter.ProductId != null)
            {
                filterQuery = filterQuery.Where(x => x.ProductId == filter.ProductId);
            }
            if (filter.ProviderId != null)
            {
                filterQuery = filterQuery.Where(x => x.ProviderId == filter.ProviderId);
            }
            if (!string.IsNullOrEmpty(filter.AuxiliaryCode))
            {
                filterQuery = filterQuery.Where(x => x.AuxiliaryCode.ToLower().Contains(filter.AuxiliaryCode.ToLower()));
            }
            if (filter.Active != null)
            {
                filterQuery = filterQuery.Where(x => x.Active == filter.Active);
            }

            return filterQuery;
        }
    }
}
