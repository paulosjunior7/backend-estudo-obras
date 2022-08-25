using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionDocumentationDomain.Enums;
using Obras.Business.ConstructionDocumentationDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionDocumentationDomain.Services
{
    public interface IConstructionDocumentationService
    {
        Task<ConstructionDocumentation> CreateAsync(ConstructionDocumentationModel model);
        Task<ConstructionDocumentation> UpdateAsync(int id, ConstructionDocumentationModel model);
        Task<PageResponse<ConstructionDocumentation>> GetAsync(PageRequest<ConstructionDocumentationFilter, ConstructionDocumentationSortingFields> pageRequest);
        Task<ConstructionDocumentation> GetId(int id);
    }
    public class ConstructionDocumentationService : IConstructionDocumentationService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionDocumentationService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionDocumentation> CreateAsync(ConstructionDocumentationModel model)
        {
            var constructionDocumentation = _mapper.Map<ConstructionDocumentation>(model);
            constructionDocumentation.CreationDate = DateTime.Now;
            constructionDocumentation.ChangeDate = DateTime.Now;

            _dbContext.ConstructionDocumentations.Add(constructionDocumentation);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionDocumentation;
        }

        public async Task<ConstructionDocumentation> UpdateAsync(int id, ConstructionDocumentationModel model)
        {
            var constructionDocumentation = await _dbContext.ConstructionDocumentations.FindAsync(id);

            if (constructionDocumentation != null)
            {
                constructionDocumentation.ChangeUserId = model.ChangeUserId;
                constructionDocumentation.Active = model.Active;
                constructionDocumentation.ConstructionId = model.ConstructionId;
                constructionDocumentation.Date = model.Date;
                constructionDocumentation.DocumentationId = model.DocumentationId;
                constructionDocumentation.Value = model.Value;
                constructionDocumentation.ConstructionInvestorId = model.ConstructionInvestorId;
                constructionDocumentation.ChangeDate = DateTime.Now;
                constructionDocumentation.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionDocumentation;
        }

        public async Task<ConstructionDocumentation> GetId(int id)
        {
            return await _dbContext.ConstructionDocumentations.FindAsync(id);
        }

        public async Task<PageResponse<ConstructionDocumentation>> GetAsync(PageRequest<ConstructionDocumentationFilter, ConstructionDocumentationSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionDocumentations.Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionDocumentation> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionDocumentation>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionDocumentation> LoadOrder(PageRequest<ConstructionDocumentationFilter, ConstructionDocumentationSortingFields> pageRequest, IQueryable<ConstructionDocumentation> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionDocumentationSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionDocumentationSortingFields.Date)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Date)
                    : dataQuery.OrderBy(x => x.Date);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionDocumentation> LoadFilterQuery(ConstructionDocumentationFilter filter, IQueryable<ConstructionDocumentation> filterQuery)
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
