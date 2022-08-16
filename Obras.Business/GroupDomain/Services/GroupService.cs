using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.GroupDomain.Enums;
using Obras.Business.GroupDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.GroupDomain.Services
{
    public interface IGroupService
    {
        Task<Group> CreateAsync(GroupModel model);
        Task<Group> UpdateAsync(int id, GroupModel model);
        Task<PageResponse<Group>> GetAsync(PageRequest<GroupFilter, GroupSortingFields> pageRequest);
        Task<Group> GetId(int id);
    }
    public class GroupService : IGroupService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public GroupService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Group> CreateAsync(GroupModel model)
        {
            var mod = _mapper.Map<Group>(model);
            mod.CreationDate = DateTime.Now;
            mod.ChangeDate = DateTime.Now;
            mod.CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId);

            _dbContext.Groups.Add(mod);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return mod;
        }

        public async Task<Group> UpdateAsync(int brandId, GroupModel model)
        {
            var mod = await _dbContext.Groups.FindAsync(brandId);

            if (mod != null)
            {
                mod.Active = model.Active;
                mod.Description = model.Description;
                mod.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return mod;
        }

        public async Task<Group> GetId(int id)
        {
            return await _dbContext.Groups.FindAsync(id);
        }

        public async Task<PageResponse<Group>> GetAsync(PageRequest<GroupFilter, GroupSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Groups.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Group> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Group>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Group> LoadOrder(PageRequest<GroupFilter, GroupSortingFields> pageRequest, IQueryable<Group> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.GroupSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.GroupSortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.GroupSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Group> LoadFilterQuery(GroupFilter filter, IQueryable<Group> filterQuery)
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
