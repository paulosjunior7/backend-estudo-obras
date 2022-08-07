using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionInvestorDomain.Enums;
using Obras.Business.ConstructionInvestorDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.ConstructionInvestorDomain.Services
{
    public interface IConstructionInvestorService
    {
        Task<ConstructionInvestor> CreateAsync(ConstructionInvestorModel model);
        Task<ConstructionInvestor> UpdateAsync(int id, ConstructionInvestorModel model);
        Task<PageResponse<ConstructionInvestor>> GetAsync(PageRequest<ConstructionInvestorFilter, ConstructionInvestorSortingFields> pageRequest);
        Task<ConstructionInvestor> GetId(int id);
    }
    public class ConstructionInvestorService : IConstructionInvestorService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public ConstructionInvestorService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ConstructionInvestor> CreateAsync(ConstructionInvestorModel model)
        {
            var constructionInvestor = _mapper.Map<ConstructionInvestor>(model);
            constructionInvestor.CreationDate = DateTime.Now;
            constructionInvestor.ChangeDate = DateTime.Now;

            _dbContext.ConstructionInvestors.Add(constructionInvestor);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return constructionInvestor;
        }

        public async Task<ConstructionInvestor> UpdateAsync(int id, ConstructionInvestorModel model)
        {
            var constructionInvestor = await _dbContext.ConstructionInvestors.FindAsync(id);

            if (constructionInvestor != null)
            {
                constructionInvestor.ChangeUserId = model.ChangeUserId;
                constructionInvestor.Active = model.Active;
                constructionInvestor.ConstructionId = model.ConstructionId;
                constructionInvestor.PeopleId = model.PeopleId;
                constructionInvestor.ChangeDate = DateTime.Now;
                constructionInvestor.ChangeUserId = model.ChangeUserId;
                await _dbContext.SaveChangesAsync();
            }

            return constructionInvestor;
        }

        public async Task<ConstructionInvestor> GetId(int id)
        {
            return await _dbContext.ConstructionInvestors.FindAsync(id);
        }

        public async Task<PageResponse<ConstructionInvestor>> GetAsync(PageRequest<ConstructionInvestorFilter, ConstructionInvestorSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.ConstructionInvestors.Include(a => a.People).Where(x => x.Id > 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<ConstructionInvestor> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<ConstructionInvestor>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<ConstructionInvestor> LoadOrder(PageRequest<ConstructionInvestorFilter, ConstructionInvestorSortingFields> pageRequest, IQueryable<ConstructionInvestor> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.ConstructionInvestorSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }
            else if (pageRequest.OrderBy?.Field == Enums.ConstructionInvestorSortingFields.NamePeople)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.People.CorporateName)
                    : dataQuery.OrderBy(x => x.People.CorporateName);
            }

            return dataQuery;
        }

        private static IQueryable<ConstructionInvestor> LoadFilterQuery(ConstructionInvestorFilter filter, IQueryable<ConstructionInvestor> filterQuery)
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
