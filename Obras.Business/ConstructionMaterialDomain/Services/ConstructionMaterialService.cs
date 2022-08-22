using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionMaterialDomain.Enums;
using Obras.Business.ConstructionMaterialDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionMaterialDomain.Services
{
    public interface IConstructionMaterialService
    {
        Task<ConstructionMaterial> CreateAsync(ConstructionMaterialModel model);
        Task<ConstructionMaterial> UpdateAsync(int id, ConstructionMaterialModel model);
        Task<PageResponse<ConstructionMaterial>> GetAsync(PageRequest<ConstructionMaterialFilter, ConstructionMaterialSortingFields> pageRequest);
        Task<ConstructionMaterial> GetId(int id);
    }
    public class ConstructionMaterialService : IConstructionMaterialService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionMaterialService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionMaterial> CreateAsync(ConstructionMaterialModel model)
        {
            var constructionMaterial = _mapper.Map<ConstructionMaterial>(model);
            constructionMaterial.CreationDate = DateTime.Now;
            constructionMaterial.ChangeDate = DateTime.Now;

            _dbContext.ConstructionMaterials.Add(constructionMaterial);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionMaterial;
        }

        public async Task<ConstructionMaterial> UpdateAsync(int id, ConstructionMaterialModel model)
        {
            var constructionMaterial = await _dbContext.ConstructionMaterials.FindAsync(id);

            if (constructionMaterial != null)
            {
                constructionMaterial.ChangeUserId = model.ChangeUserId;
                constructionMaterial.Active = model.Active;
                constructionMaterial.ConstructionId = model.ConstructionId;
                constructionMaterial.BrandId = model.BrandId;
                constructionMaterial.ConstructionInvestorId = model.ConstructionInvestorId;
                constructionMaterial.GroupId = model.GroupId;
                constructionMaterial.ProductId = model.ProductId;
                constructionMaterial.UnityId = model.UnityId;
                constructionMaterial.ProviderId = model.ProviderId;
                constructionMaterial.PurchaseDate = model.PurchaseDate;
                constructionMaterial.Quantity = model.Quantity;
                constructionMaterial.UnitPrice = model.UnitPrice;
                constructionMaterial.ChangeDate = DateTime.Now;
                constructionMaterial.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionMaterial;
        }

        public async Task<ConstructionMaterial> GetId(int id)
        {
            return await _dbContext.ConstructionMaterials.FindAsync(id);
        }

        public async Task<PageResponse<ConstructionMaterial>> GetAsync(PageRequest<ConstructionMaterialFilter, ConstructionMaterialSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionMaterials.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionMaterial> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionMaterial>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionMaterial> LoadOrder(PageRequest<ConstructionMaterialFilter, ConstructionMaterialSortingFields> pageRequest, IQueryable<ConstructionMaterial> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionMaterialSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionMaterialSortingFields.PurchaseDate)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.PurchaseDate)
                    : dataQuery.OrderBy(x => x.PurchaseDate);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionMaterial> LoadFilterQuery(ConstructionMaterialFilter filter, IQueryable<ConstructionMaterial> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.ConstructionId != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.ConstructionId);
            }
            if (filter.GroupId != null)
            {
                filterQuery = filterQuery.Where(x => x.GroupId == filter.GroupId);
            }
            if (filter.ProductId != null)
            {
                filterQuery = filterQuery.Where(x => x.ProductId == filter.ProductId);
            }
            if (filter.PurchaseDate != null)
            {
                filterQuery = filterQuery.Where(x => x.PurchaseDate == filter.PurchaseDate);
            }
            if (filter.Quantity != null)
            {
                filterQuery = filterQuery.Where(x => x.Quantity == filter.Quantity);
            }
            if (filter.UnitPrice != null)
            {
                filterQuery = filterQuery.Where(x => x.UnitPrice == filter.UnitPrice);
            }

            return filterQuery;
        }
    }
}
