using Microsoft.EntityFrameworkCore;
using Obras.Business.ResponsibilityDomain.Enums;
using Obras.Business.ResponsibilityDomain.Models;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ResponsibilityDomain.Services
{
    public interface IResponsibilityService
    {
        Task<Responsibility> CreateAsync(ResponsibilityModel model);
        Task<Responsibility> UpdateResponsibilityAsync(int id, ResponsibilityModel model);
        Task<PageResponse<Responsibility>> GetResponsibilitiesAsync(PageRequest<ResponsibilityFilter, ResponsibilitySortingFields> pageRequest);
        Task<Responsibility> GetResponsibilityId(int id);
    }

    public class ResponsibilityService : IResponsibilityService
    {
        private readonly ObrasDBContext _dbContext;

        public ResponsibilityService(ObrasDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Responsibility> CreateAsync(ResponsibilityModel model)
        {
            var res = new Responsibility
            {
                Description = model.Description,
                CreationDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                Active = model.Active,
                RegistrationUserId = model.RegistrationUserId,
                ChangeUserId = model.ChangeUserId,
                CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId)
            };

            _dbContext.Responsibilities.Add(res);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }

        public async Task<Responsibility> UpdateResponsibilityAsync(int id, ResponsibilityModel model)
        {
            var doc = await _dbContext.Responsibilities.FindAsync(id);

            if (doc != null)
            {
                doc.Active = model.Active;
                doc.Description = model.Description;
                doc.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return doc;
        }

        public async Task<Responsibility> GetResponsibilityId(int id)
        {
            return await _dbContext.Responsibilities.FindAsync(id);
        }

        public async Task<PageResponse<Responsibility>> GetResponsibilitiesAsync(PageRequest<ResponsibilityFilter, ResponsibilitySortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Responsibilities.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            int totalCount = await dataQuery.CountAsync();

            List<Responsibility> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Responsibility>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Responsibility> LoadOrder(PageRequest<ResponsibilityFilter, ResponsibilitySortingFields> pageRequest, IQueryable<Responsibility> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ResponsibilitySortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ResponsibilitySortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ResponsibilitySortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Responsibility> LoadFilterQuery(ResponsibilityFilter filter, IQueryable<Responsibility> filterQuery)
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
