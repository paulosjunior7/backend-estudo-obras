using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionHouseDomain.Enums;
using Obras.Business.ConstructionHouseDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionHouseDomain.Services
{
    public interface IConstructionHouseService
    {
        Task<ConstructionHouse> CreateAsync(ConstructionHouseModel model);
        Task<ConstructionHouse> UpdateAsync(int id, ConstructionHouseModel model);
        Task<PageResponse<ConstructionHouse>> GetAsync(PageRequest<ConstructionHouseFilter, ConstructionHouseSortingFields> pageRequest);
        Task<ConstructionHouse> GetId(int constructionId, int id);
    }

    public class ConstructionHouseService : IConstructionHouseService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionHouseService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionHouse> CreateAsync(ConstructionHouseModel model)
        {
            var constructionHouse = _mapper.Map<ConstructionHouse>(model);
            constructionHouse.CreationDate = DateTime.Now;
            constructionHouse.ChangeDate = DateTime.Now;

            _dbContext.ConstructionHouses.Add(constructionHouse);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionHouse;
        }

        public async Task<ConstructionHouse> UpdateAsync(int id, ConstructionHouseModel model)
        {
            var constructionHouse = await _dbContext.ConstructionHouses.FindAsync(id);

            if (constructionHouse != null)
            {
                constructionHouse.ChangeUserId = model.ChangeUserId;
                constructionHouse.Active = model.Active;
                constructionHouse.ConstructionId = model.ConstructionId;
                constructionHouse.BuildingArea = model.BuildingArea;
                constructionHouse.EnergyConsumptionUnit = model.EnergyConsumptionUnit;
                constructionHouse.FractionBatch = model.FractionBatch;
                constructionHouse.PermeableArea = model.PermeableArea;
                constructionHouse.SaleValue = model.SaleValue;
                constructionHouse.WaterConsumptionUnit = model.WaterConsumptionUnit;
                constructionHouse.ChangeDate = DateTime.Now;
                constructionHouse.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionHouse;
        }

        public async Task<ConstructionHouse> GetId(int constructionId, int id)
        {
            return await _dbContext.ConstructionHouses.Where(c => c.Id == id && c.ConstructionId == constructionId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<PageResponse<ConstructionHouse>> GetAsync(PageRequest<ConstructionHouseFilter, ConstructionHouseSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionHouses.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionHouse> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionHouse>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionHouse> LoadOrder(PageRequest<ConstructionHouseFilter, ConstructionHouseSortingFields> pageRequest, IQueryable<ConstructionHouse> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionHouseSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionHouseSortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionHouse> LoadFilterQuery(ConstructionHouseFilter filter, IQueryable<ConstructionHouse> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (filter.ConstructionId != null)
            {
                filterQuery = filterQuery.Where(x => x.ConstructionId == filter.ConstructionId);
            }
            if (filter.BuildingArea != null)
            {
                filterQuery = filterQuery.Where(x => x.BuildingArea == filter.BuildingArea);
            }
            if (filter.EnergyConsumptionUnit != null)
            {
                filterQuery = filterQuery.Where(x => x.EnergyConsumptionUnit == filter.EnergyConsumptionUnit);
            }
            if (filter.PermeableArea != null)
            {
                filterQuery = filterQuery.Where(x => x.PermeableArea == filter.PermeableArea);
            }
            if (filter.Registration != null)
            {
                filterQuery = filterQuery.Where(x => x.Registration == filter.Registration);
            }
            if (filter.SaleValue != null)
            {
                filterQuery = filterQuery.Where(x => x.SaleValue == filter.SaleValue);
            }
            if (filter.WaterConsumptionUnit != null)
            {
                filterQuery = filterQuery.Where(x => x.WaterConsumptionUnit == filter.WaterConsumptionUnit);
            }

            return filterQuery;
        }
    }
}
