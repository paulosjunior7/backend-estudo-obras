using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionBatchDomain.Enums;
using Obras.Business.ConstructionBatchDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionBatchDomain.Services
{
    public interface IConstructionBatchService
    {
        Task<ConstructionBatch> CreateAsync(ConstructionBatchModel model);
        Task<ConstructionBatch> UpdateAsync(int id, ConstructionBatchModel model);
        Task<PageResponse<ConstructionBatch>> GetAsync(PageRequest<ConstructionBatchFilter, ConstructionBatchSortingFields> pageRequest);
        Task<ConstructionBatch> GetId(int constructionId, int id);
    }
    public class ConstructionBatchService : IConstructionBatchService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionBatchService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionBatch> CreateAsync(ConstructionBatchModel model)
        {
            var constructionBatch = _mapper.Map<ConstructionBatch>(model);
            constructionBatch.CreationDate = DateTime.Now;
            constructionBatch.ChangeDate = DateTime.Now;

            _dbContext.ConstructionBatchs.Add(constructionBatch);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionBatch;
        }

        public async Task<ConstructionBatch> UpdateAsync(int id, ConstructionBatchModel model)
        {
            var constructionBatch = await _dbContext.ConstructionBatchs.FindAsync(id);

            if (constructionBatch != null)
            {
                constructionBatch.ChangeUserId = model.ChangeUserId;
                constructionBatch.Active = model.Active;
                constructionBatch.Value = model.Value;
                constructionBatch.ConstructionId = model.ConstructionId;
                constructionBatch.PeopleId = model.PeopleId;
                constructionBatch.ChangeDate = DateTime.Now;
                constructionBatch.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionBatch;
        }

        public async Task<ConstructionBatch> GetId(int constructionId, int id)
        {
            return await _dbContext.ConstructionBatchs.Where(c => c.Id == id && c.ConstructionId == constructionId).FirstOrDefaultAsync();
        }

        public async Task<PageResponse<ConstructionBatch>> GetAsync(PageRequest<ConstructionBatchFilter, ConstructionBatchSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionBatchs.Include(a => a.People).Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionBatch> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionBatch>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionBatch> LoadOrder(PageRequest<ConstructionBatchFilter, ConstructionBatchSortingFields> pageRequest, IQueryable<ConstructionBatch> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionBatchSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionBatchSortingFields.NamePeople)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.People.CorporateName)
                    : dataQuery.OrderBy(x => x.People.CorporateName);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionBatch> LoadFilterQuery(ConstructionBatchFilter filter, IQueryable<ConstructionBatch> filterQuery)
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
            if (filter.PeopleId != null)
            {
                filterQuery = filterQuery.Where(x => x.PeopleId == filter.PeopleId);
            }

            return filterQuery;
        }
    }
}
