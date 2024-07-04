using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.BrandDomain.Enums;
using Obras.Business.BrandDomain.Models;
using Obras.Business.BrandDomain.Response;
using Obras.Business.ConstructionDocumentationDomain.Response;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.BrandDomain.Services
{
    public interface IBrandService
    {
        Task<Brand> CreateAsync(BrandModel brand);
        Task<Brand> UpdateBrandAsync(int brandId, BrandModel brand);
        Task<PageResponse<BrandResponse>> GetBrandsAsync(PageRequest<BrandFilter, BrandSortingFields> pageRequest);
        Task<BrandResponse> GetBrandId(int id);
    }

    public class BrandService : IBrandService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public BrandService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Brand> CreateAsync(BrandModel brand)
        {
            var bra = _mapper.Map<Brand>(brand);
            bra.CreationDate = DateTime.Now;
            bra.ChangeDate = DateTime.Now;
            bra.CompanyId = (int)(brand.CompanyId == null ? 0 : brand.CompanyId);

            _dbContext.Brands.Add(bra);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return bra;
        }

        public async Task<Brand> UpdateBrandAsync(int brandId, BrandModel brand)
        {
            var bra = await _dbContext.Brands.FindAsync(brandId);

            if (bra != null)
            {
                bra.Active = brand.Active;
                bra.Description = brand.Description;
                bra.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return bra;
        }

        public async Task<BrandResponse> GetBrandId(int id)
        {
            var response =  await _dbContext.Brands.FindAsync(id);

            return _mapper.Map<BrandResponse>(response);
        }

        public async Task<PageResponse<BrandResponse>> GetBrandsAsync(PageRequest<BrandFilter, BrandSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Brands.AsNoTracking().Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Brand> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            var branchs = _mapper.Map<List<BrandResponse>>(nodes);

            return new PageResponse<BrandResponse>
            {
                Nodes = branchs,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Brand> LoadOrder(PageRequest<BrandFilter, BrandSortingFields> pageRequest, IQueryable<Brand> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.BrandSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.BrandSortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.BrandSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Brand> LoadFilterQuery(BrandFilter filter, IQueryable<Brand> filterQuery)
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
            if (filter.Active != null)
            {
                filterQuery = filterQuery.Where(x => x.Active == filter.Active);
            }

            return filterQuery;
        }
    }
}
