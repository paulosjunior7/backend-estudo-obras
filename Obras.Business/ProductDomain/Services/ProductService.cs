using Microsoft.EntityFrameworkCore;
using Obras.Business.ProductDomain.Enums;
using Obras.Business.ProductDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obras.Business.SharedDomain.Models;
using Obras.Business.SharedDomain.Enums;
using AutoMapper;

namespace Obras.Business.ProductDomain.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(ProductModel product);
        Task<Product> UpdateProductAsync(int productId, ProductModel product);
        Task<PageResponse<Product>> GetProductsAsync(PageRequest<ProductFilter, ProductSortingFields> pageRequest);
        Task<Product> GetProductId(int id);
    }

    public class ProductService : IProductService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Product> CreateAsync(ProductModel product)
        {
            var prod = _mapper.Map<Product>(product);
            prod.CreationDate = DateTime.Now;
            prod.ChangeDate = DateTime.Now;
            prod.CompanyId = (int)(product.CompanyId == null ? 0 : product.CompanyId);

            _dbContext.Products.Add(prod);
            try
            {
                var retorno =  await _dbContext.SaveChangesAsync();
            } catch (Exception e)
            {
                throw e;
            }
            return prod;
        }

        public async Task<Product> UpdateProductAsync(int productId, ProductModel product)
        {
            var prod = await _dbContext.Products.FindAsync(productId);

            if (prod != null)
            {
                prod.Active = product.Active;
                prod.Description = product.Description;
                prod.Detail = product.Detail;
                prod.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return prod;
        }

        public async Task<Product> GetProductId(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<PageResponse<Product>> GetProductsAsync(PageRequest<ProductFilter, ProductSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Products.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Product> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Product>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Product> LoadOrder(PageRequest<ProductFilter, ProductSortingFields> pageRequest, IQueryable<Product> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ProductSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction ==  SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProductSortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ProductSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Product> LoadFilterQuery(ProductFilter filter, IQueryable<Product> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.Description))
            {
                filterQuery = filterQuery.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.Detail))
            {
                filterQuery = filterQuery.Where(x => x.Detail.ToLower().Contains(filter.Detail.ToLower()));
            }
            if (filter.Active != null)
            {
                filterQuery = filterQuery.Where(x => x.Active == filter.Active);
            }

            return filterQuery;
        }
    }
}
