using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.DocumentationDomain.Enums;
using Obras.Business.DocumentationDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.DocumentationDomain.Services
{
    public interface IDocumentationService
    {
        Task<Documentation> CreateAsync(DocumentationModel documentation);
        Task<Documentation> UpdateDocumentationAsync(int documentationId, DocumentationModel documentation);
        Task<PageResponse<Documentation>> GetDocumentationsAsync(PageRequest<DocumentationFilter, DocumentationSortingFields> pageRequest);
        Task<Documentation> GetDocumentationId(int id);
    }

    public class DocumentationService : IDocumentationService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public DocumentationService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Documentation> CreateAsync(DocumentationModel documentation)
        {
            var doc = _mapper.Map<Documentation>(documentation);
            doc.CreationDate = DateTime.Now;
            doc.ChangeDate = DateTime.Now;
            doc.CompanyId = (int)(documentation.CompanyId == null ? 0 : documentation.CompanyId);

            _dbContext.Documentations.Add(doc);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return doc;
        }

        public async Task<Documentation> UpdateDocumentationAsync(int documentationId, DocumentationModel documentation)
        {
            var doc = await _dbContext.Documentations.FindAsync(documentationId);

            if (doc != null)
            {
                doc.Active = documentation.Active;
                doc.Description = documentation.Description;
                doc.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return doc;
        }

        public async Task<Documentation> GetDocumentationId(int id)
        {
            return await _dbContext.Documentations.FindAsync(id);
        }

        public async Task<PageResponse<Documentation>> GetDocumentationsAsync(PageRequest<DocumentationFilter, DocumentationSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Documentations.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Documentation> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Documentation>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Documentation> LoadOrder(PageRequest<DocumentationFilter, DocumentationSortingFields> pageRequest, IQueryable<Documentation> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.DocumentationSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.DocumentationSortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.DocumentationSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Documentation> LoadFilterQuery(DocumentationFilter filter, IQueryable<Documentation> filterQuery)
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
