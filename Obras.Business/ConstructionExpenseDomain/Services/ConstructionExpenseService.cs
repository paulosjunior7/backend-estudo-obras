using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionExpenseDomain.Enums;
using Obras.Business.ConstructionExpenseDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionExpenseDomain.Services
{
    public interface IConstructionExpenseService
    {
        Task<ConstructionExpense> CreateAsync(ConstructionExpenseModel model);
        Task<ConstructionExpense> UpdateAsync(int id, ConstructionExpenseModel model);
        Task<PageResponse<ConstructionExpense>> GetAsync(PageRequest<ConstructionExpenseFilter, ConstructionExpenseSortingFields> pageRequest);
        Task<ConstructionExpense> GetId(int id);
    }
    public class ConstructionExpenseService : IConstructionExpenseService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionExpenseService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionExpense> CreateAsync(ConstructionExpenseModel model)
        {
            var constructionExpense = _mapper.Map<ConstructionExpense>(model);
            constructionExpense.CreationDate = DateTime.Now;
            constructionExpense.ChangeDate = DateTime.Now;

            _dbContext.ConstructionExpenses.Add(constructionExpense);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionExpense;
        }

        public async Task<ConstructionExpense> UpdateAsync(int id, ConstructionExpenseModel model)
        {
            var constructionExpense = await _dbContext.ConstructionExpenses.FindAsync(id);

            if (constructionExpense != null)
            {
                constructionExpense.ChangeUserId = model.ChangeUserId;
                constructionExpense.Active = model.Active;
                constructionExpense.ConstructionId = model.ConstructionId;
                constructionExpense.Date = model.Date;
                constructionExpense.ExpenseId = model.ExpenseId;
                constructionExpense.Value = model.Value;
                constructionExpense.ConstructionInvestorId = model.ConstructionInvestorId;
                constructionExpense.ChangeDate = DateTime.Now;
                constructionExpense.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionExpense;
        }

        public async Task<ConstructionExpense> GetId(int id)
        {
            return await _dbContext.ConstructionExpenses.FindAsync(id);
        }

        public async Task<PageResponse<ConstructionExpense>> GetAsync(PageRequest<ConstructionExpenseFilter, ConstructionExpenseSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionExpenses.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionExpense> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionExpense>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionExpense> LoadOrder(PageRequest<ConstructionExpenseFilter, ConstructionExpenseSortingFields> pageRequest, IQueryable<ConstructionExpense> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionExpenseSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionExpenseSortingFields.Date)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Date)
                    : dataQuery.OrderBy(x => x.Date);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionExpense> LoadFilterQuery(ConstructionExpenseFilter filter, IQueryable<ConstructionExpense> filterQuery)
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
