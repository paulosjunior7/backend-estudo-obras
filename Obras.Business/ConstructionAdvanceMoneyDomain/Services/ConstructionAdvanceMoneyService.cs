using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionAdvanceMoneyDomain.Enums;
using Obras.Business.ConstructionAdvanceMoneyDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionAdvanceMoneyDomain.Services
{
    public interface IConstructionAdvanceMoneyService
    {
        Task<ConstructionAdvanceMoney> CreateAsync(ConstructionAdvanceMoneyModel model);
        Task<ConstructionAdvanceMoney> UpdateAsync(int id, ConstructionAdvanceMoneyModel model);
        Task<PageResponse<ConstructionAdvanceMoney>> GetAsync(PageRequest<ConstructionAdvanceMoneyFilter, ConstructionAdvanceMoneySortingFields> pageRequest);
        Task<ConstructionAdvanceMoney?> GetId(int constructionId, int id);
    }
    public class ConstructionAdvanceMoneyService : IConstructionAdvanceMoneyService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionAdvanceMoneyService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionAdvanceMoney> CreateAsync(ConstructionAdvanceMoneyModel model)
        {
            var constructionAdvanceMoney = _mapper.Map<ConstructionAdvanceMoney>(model);
            constructionAdvanceMoney.CreationDate = DateTime.Now;
            constructionAdvanceMoney.ChangeDate = DateTime.Now;

            _dbContext.ConstructionAdvancesMoney.Add(constructionAdvanceMoney);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionAdvanceMoney;
        }

        public async Task<ConstructionAdvanceMoney> UpdateAsync(int id, ConstructionAdvanceMoneyModel model)
        {
            var constructionAdvanceMoney = await _dbContext.ConstructionAdvancesMoney.FindAsync(id);

            if (constructionAdvanceMoney != null)
            {
                constructionAdvanceMoney.ChangeUserId = model.ChangeUserId;
                constructionAdvanceMoney.Active = model.Active;
                constructionAdvanceMoney.ConstructionId = model.ConstructionId;
                constructionAdvanceMoney.Date = model.Date;
                constructionAdvanceMoney.Value = model.Value;
                constructionAdvanceMoney.ConstructionInvestorId = model.ConstructionInvestorId;
                constructionAdvanceMoney.ChangeDate = DateTime.Now;
                constructionAdvanceMoney.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionAdvanceMoney;
        }

        public async Task<ConstructionAdvanceMoney?> GetId(int constructionId, int id)
        {
            return await _dbContext.ConstructionAdvancesMoney.Where(c => c.Id == id && c.ConstructionId == constructionId).FirstOrDefaultAsync();
        }

        public async Task<PageResponse<ConstructionAdvanceMoney>> GetAsync(PageRequest<ConstructionAdvanceMoneyFilter, ConstructionAdvanceMoneySortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionAdvancesMoney.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionAdvanceMoney> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionAdvanceMoney>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionAdvanceMoney> LoadOrder(PageRequest<ConstructionAdvanceMoneyFilter, ConstructionAdvanceMoneySortingFields> pageRequest, IQueryable<ConstructionAdvanceMoney> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionAdvanceMoneySortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionAdvanceMoneySortingFields.Date)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Date)
                    : dataQuery.OrderBy(x => x.Date);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionAdvanceMoney> LoadFilterQuery(ConstructionAdvanceMoneyFilter filter, IQueryable<ConstructionAdvanceMoney> filterQuery)
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
