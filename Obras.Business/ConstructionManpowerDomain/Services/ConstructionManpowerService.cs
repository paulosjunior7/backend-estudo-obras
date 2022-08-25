using Obras.Data.Entities;
using Obras.Business.SharedDomain.Models;
using System.Threading.Tasks;
using Obras.Business.ConstructionManpowerDomain.Models;
using Obras.Business.ConstructionManpowerDomain.Enums;
using Obras.Data;
using AutoMapper;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Obras.Business.SharedDomain.Enums;
using System.Collections.Generic;

namespace Obras.Business.ConstructionManpowerDomain.Services
{
    public interface IConstructionManpowerService
    {
        Task<ConstructionManpower> CreateAsync(ConstructionManpowerModel model);
        Task<ConstructionManpower> UpdateAsync(int id, ConstructionManpowerModel model);
        Task<PageResponse<ConstructionManpower>> GetAsync(PageRequest<ConstructionManpowerFilter, ConstructionManpowerSortingFields> pageRequest);
        Task<ConstructionManpower> GetId(int id);
    }

    public class ConstructionManpowerService: IConstructionManpowerService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionManpowerService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionManpower> CreateAsync(ConstructionManpowerModel model)
        {
            var constructionManpower = _mapper.Map<ConstructionManpower>(model);
            constructionManpower.CreationDate = DateTime.Now;
            constructionManpower.ChangeDate = DateTime.Now;

            _dbContext.ConstructionManpowers.Add(constructionManpower);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionManpower;
        }

        public async Task<ConstructionManpower> UpdateAsync(int id, ConstructionManpowerModel model)
        {
            var constructionManpower = await _dbContext.ConstructionManpowers.FindAsync(id);

            if (constructionManpower != null)
            {
                constructionManpower.ChangeUserId = model.ChangeUserId;
                constructionManpower.Active = model.Active;
                constructionManpower.ConstructionId = model.ConstructionId;
                constructionManpower.Date = model.Date;
                constructionManpower.EmployeeId = model.EmployeeId;
                constructionManpower.OutsourcedId = model.OutsourcedId;
                constructionManpower.Value = model.Value;
                constructionManpower.ConstructionInvestorId = model.ConstructionInvestorId;
                constructionManpower.ChangeDate = DateTime.Now;
                constructionManpower.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionManpower;
        }

        public async Task<ConstructionManpower> GetId(int id)
        {
            return await _dbContext.ConstructionManpowers.FindAsync(id);
        }

        public async Task<PageResponse<ConstructionManpower>> GetAsync(PageRequest<ConstructionManpowerFilter, ConstructionManpowerSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionManpowers.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionManpower> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionManpower>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionManpower> LoadOrder(PageRequest<ConstructionManpowerFilter, ConstructionManpowerSortingFields> pageRequest, IQueryable<ConstructionManpower> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionManpowerSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionManpowerSortingFields.Date)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Date)
                    : dataQuery.OrderBy(x => x.Date);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionManpower> LoadFilterQuery(ConstructionManpowerFilter filter, IQueryable<ConstructionManpower> filterQuery)
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

            return filterQuery;
        }
    }
}
