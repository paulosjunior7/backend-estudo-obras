using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionDomain.Enums;
using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionDomain.Services
{
    public interface IConstructionService
    {
        Task<Construction> CreateAsync(ConstructionModel model);
        Task<Construction> UpdateAsync(int id, ConstructionModel model);
        Task<Construction> UpdateAddressAsync(int id, User user, ConstructionAddressInput input);
        Task<Construction> UpdateDateAsync(int id, User user, ConstructionDateInput input);
        Task<Construction> UpdateDetailsAsync(int id, User user, ConstructionDetailsInput input);
        Task<PageResponse<Construction>> GetAsync(PageRequest<ConstructionFilter, ConstructionSortingFields> pageRequest);
        Task<Construction> GetId(int id);
    }

    public class ConstructionService : IConstructionService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Construction> CreateAsync(ConstructionModel model)
        {
            var construction = _mapper.Map<Construction>(model);
            construction.CreationDate = DateTime.Now;
            construction.ChangeDate = DateTime.Now;
            construction.CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId);

            _dbContext.Constructions.Add(construction);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return construction;
        }

        public async Task<Construction> UpdateAsync(int id, ConstructionModel model)
        {
            var construction = await _dbContext.Constructions.FindAsync(id);

            if (construction != null)
            {
                construction.Address = model.Address;
                construction.Active = model.Active;
                construction.Art = model.Art;
                construction.BatchArea = model.BatchArea;
                construction.BuildingArea = model.BuildingArea;
                construction.ChangeDate = DateTime.Now;
                construction.ChangeUserId = model.ChangeUserId;
                construction.Cno = model.Cno;
                construction.CompanyId = model.CompanyId;
                construction.Complement = model.Complement;
                construction.DateBegin = model.DateBegin;
                construction.DateEnd = model.DateEnd;
                construction.Identifier = model.Identifier;
                construction.Number = model.Number;
                construction.City = model.City;
                construction.State = model.State;
                construction.Latitude = model.Latitude;
                construction.Longitude = model.Longitude;
                construction.License = model.License;
                construction.MotherEnrollment = model.MotherEnrollment;
                construction.MunicipalRegistration = model.MunicipalRegistration;
                construction.Neighbourhood = model.Neighbourhood;
                construction.SaleValue = model.SaleValue;
                construction.StatusConstruction = model.StatusConstruction;
                construction.UndergroundUse = model.UndergroundUse;
                construction.ZipCode = model.ZipCode;
                construction.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return construction;
        }

        public async Task<Construction> GetId(int id)
        {
            return await _dbContext.Constructions.FindAsync(id);
        }

        public async Task<PageResponse<Construction>> GetAsync(PageRequest<ConstructionFilter, ConstructionSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Constructions.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Construction> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Construction>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Construction> LoadOrder(PageRequest<ConstructionFilter, ConstructionSortingFields> pageRequest, IQueryable<Construction> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionSortingFields.StatusConstruction)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.StatusConstruction)
                    : dataQuery.OrderBy(x => x.StatusConstruction);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionSortingFields.Identifier)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Identifier)
                    : dataQuery.OrderBy(x => x.Identifier);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionSortingFields.DateEnd)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.DateEnd)
                    : dataQuery.OrderBy(x => x.DateEnd);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionSortingFields.DateBegin)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.DateBegin)
                    : dataQuery.OrderBy(x => x.DateBegin);
            }

            return dataQuery;
        }

        private static IQueryable<Construction> LoadFilterQuery(ConstructionFilter filter, IQueryable<Construction> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (filter.DateEnd != null)
            {
                filterQuery = filterQuery.Where(x => x.DateEnd == filter.DateEnd);
            }
            if (filter.DateBegin != null)
            {
                filterQuery = filterQuery.Where(x => x.DateBegin == filter.DateBegin);
            }
            if (!string.IsNullOrEmpty(filter.City))
            {
                filterQuery = filterQuery.Where(x => x.City.Contains(filter.City));
            }
            if (filter.Active != null)
            {
                filterQuery = filterQuery.Where(x => x.Active == filter.Active);
            }
            if (!string.IsNullOrEmpty(filter.Identifier))
            {
                filterQuery = filterQuery.Where(x => x.Identifier.Contains(filter.Identifier.ToLower()));
            }
            if (filter.StatusConstruction != null)
            {
                filterQuery = filterQuery.Where(x => x.StatusConstruction == filter.StatusConstruction);
            }

            return filterQuery;
        }

        public async Task<Construction> UpdateAddressAsync(int id, User user, ConstructionAddressInput input)
        {
            var construction = await _dbContext.Constructions.FindAsync(id);

            if (construction != null)
            {
                construction.Address = input.Address;
                construction.City = input.City;
                construction.Complement = input.Complement;
                construction.Neighbourhood = input.Neighbourhood;
                construction.State = input.State;
                construction.Number = input.Number;
                construction.ZipCode = input.ZipCode;
                construction.ChangeUserId = user.Id;
                await _dbContext.SaveChangesAsync();
            }

            return construction;
        }

        public async Task<Construction> UpdateDateAsync(int id, User user, ConstructionDateInput input)
        {
            var construction = await _dbContext.Constructions.FindAsync(id);

            if (construction != null)
            {
                construction.Identifier = input.Identifier;
                construction.DateBegin = input.DateBegin;
                construction.DateEnd = input.DateEnd;
                construction.StatusConstruction = input.StatusConstruction;
                construction.ChangeUserId = user.Id;
                await _dbContext.SaveChangesAsync();
            }

            return construction;
        }

        public async Task<Construction> UpdateDetailsAsync(int id, User user, ConstructionDetailsInput input)
        {
            var construction = await _dbContext.Constructions.FindAsync(id);

            if (construction != null)
            {
                construction.UndergroundUse = input.UndergroundUse;
                construction.SaleValue = input.SaleValue;
                construction.MunicipalRegistration = input.MunicipalRegistration;
                construction.MotherEnrollment = input.MotherEnrollment;
                construction.Longitude = input.Longitude;
                construction.License = input.License;
                construction.Latitude = input.Latitude;
                construction.Cno = input.Cno;
                construction.BuildingArea = input.BuildingArea;
                construction.BatchArea = input.BatchArea;
                construction.Art = input.Art;
                construction.ChangeUserId = user.Id;
                await _dbContext.SaveChangesAsync();
            }

            return construction;
        }
    }
}
