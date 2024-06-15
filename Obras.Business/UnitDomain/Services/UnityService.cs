using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.SharedDomain.Models;
using Obras.Business.UnitDomain.Enums;
using Obras.Business.UnitDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.UnitDomain.Services
{
    public interface IUnityService
    {
        Task<Unity> CreateAsync(UnityModel model);
        Task<Unity> UpdateAsync(int CompanyId, int id, UnityModel model);
        Task<PageResponse<Unity>> GetAsync(PageRequest<UnityFilter, UnitySortingFields> pageRequest);
        Task<UnityModel> GetId(int CompanyId, int id);
    }
    public class UnityService : IUnityService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public UnityService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unity> CreateAsync(UnityModel model)
        {
            var res = _mapper.Map<Unity>(model);
            res.CreationDate = DateTime.Now;
            res.ChangeDate = DateTime.Now;
            res.CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId);

            if (model.CompanyId == null)
            {
                throw new Exception("O campo CompanyId não pode ser nulo.");
            }

            var unityExists = _dbContext.Unities.Where(u => u.Active == true && u.Description == res.Description && u.Multiplier == res.Multiplier && u.CompanyId == res.CompanyId).ToListAsync();

            if (unityExists.Result.Count > 0)
            {
                throw new Exception("Já existe uma Unidade com esta descrição e multiplicador");
            }

            _dbContext.Unities.Add(res);
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

        public async Task<Unity> UpdateAsync(int CompanyId, int id, UnityModel model)
        {
            var unity = await _dbContext.Unities.FindAsync(id);

            if (unity.CompanyId != CompanyId)
            {
                throw new Exception("Unidade não encontrada.");
            }

            if (unity != null)
            {
                unity.Active = model.Active;
                unity.Description = model.Description;
                unity.Multiplier = model.Multiplier;
                unity.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return unity;
        }

        public async Task<UnityModel> GetId(int CompanyId, int id)
        {
            var result = await _dbContext.Unities.FindAsync(id);
            if (result != null && result.CompanyId != CompanyId)
            {
                return null;
            }
            return this._mapper.Map<UnityModel>(result);
        }

        public async Task<PageResponse<Unity>> GetAsync(PageRequest<UnityFilter, UnitySortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Unities.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Unity> nodes = pageRequest.Pagination != null ? await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync() : await dataQuery.ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination != null ? pageRequest.Pagination.PageNumber : 0) * (pageRequest.Pagination != null ? pageRequest.Pagination.PageSize : 0));
            bool hasPrevPage = pageRequest.Pagination != null ? pageRequest.Pagination.PageNumber > 1 : false;

            #endregion

            return new PageResponse<Unity>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Unity> LoadOrder(PageRequest<UnityFilter, UnitySortingFields> pageRequest, IQueryable<Unity> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.UnitySortingFields.Description)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Description)
                    : dataQuery.OrderBy(x => x.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.UnitySortingFields.Multiplier)
            {
                dataQuery = (pageRequest.OrderBy.Direction == Business.SharedDomain.Enums.SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Multiplier)
                    : dataQuery.OrderBy(x => x.Multiplier);
            }

            return dataQuery;
        }

        private static IQueryable<Unity> LoadFilterQuery(UnityFilter filter, IQueryable<Unity> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Multiplier != null)
            {
                filterQuery = filterQuery.Where(x => x.Multiplier == filter.Multiplier);
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
