using Microsoft.EntityFrameworkCore;
using Obras.Business.ExpenseDomain.Enums;
using Obras.Business.ExpenseDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ExpenseDomain.Services
{
    public interface IExpenseService
    {
        Task<Expense> CreateAsync(ExpenseModel model);
        Task<Expense> UpdateExpenseAsync(int id, ExpenseModel model);
        Task<PageResponse<Expense>> GetExpensesAsync(PageRequest<ExpenseFilter, ExpenseSortingFields> pageRequest);
        Task<Expense> GetExpenseId(int id);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly ObrasDBContext _dbContext;

        public ExpenseService(ObrasDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense> CreateAsync(ExpenseModel model)
        {
            var exp = new Expense
            {
                Description = model.Description,
                CreationDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                Active = model.Active,
                TypeExpense = model.TypeExpense,
                RegistrationUserId = model.RegistrationUserId,
                ChangeUserId = model.ChangeUserId,
                CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId)
            };

            _dbContext.Expenses.Add(exp);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return exp;
        }

        public async Task<Expense> UpdateExpenseAsync(int id, ExpenseModel model)
        {
            var exp = await _dbContext.Expenses.FindAsync(id);

            if (exp != null)
            {
                exp.Active = model.Active;
                exp.Description = model.Description;
                exp.TypeExpense = model.TypeExpense;
                exp.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return exp;
        }

        public async Task<Expense> GetExpenseId(int id)
        {
            return await _dbContext.Expenses.FindAsync(id);
        }

        public async Task<PageResponse<Expense>> GetExpensesAsync(PageRequest<ExpenseFilter, ExpenseSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Expenses.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            int totalCount = await dataQuery.CountAsync();

            List<Expense> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Expense>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Expense> LoadOrder(PageRequest<ExpenseFilter, ExpenseSortingFields> pageRequest, IQueryable<Expense> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ExpenseSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ExpenseSortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ExpenseSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ExpenseSortingFields.TypeExpense)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.TypeExpense)
                    : dataQuery.OrderBy(x => x.TypeExpense);
            }


            return dataQuery;
        }

        private static IQueryable<Expense> LoadFilterQuery(ExpenseFilter filter, IQueryable<Expense> filterQuery)
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
            if (filter.TypeExpense != null)
            {
                filterQuery = filterQuery.Where(x => x.TypeExpense == filter.TypeExpense);
            }

            return filterQuery;
        }
    }
}
