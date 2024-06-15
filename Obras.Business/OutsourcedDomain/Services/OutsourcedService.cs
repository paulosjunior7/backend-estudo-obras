using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.OutsourcedDomain.Models;
using Obras.Business.OutsoursedDomain.Enums;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.OutsourcedDomain.Services
{
    public interface IOutsourcedService
    {
        Task<Outsourced> CreateAsync(OutsourcedModel model);
        Task<Outsourced> UpdateOutsourcedAsync(int id, OutsourcedModel model);
        Task<PageResponse<Outsourced>> GetOutsourcedsAsync(PageRequest<OutsourcedFilter, OutsourcedSortingFields> pageRequest);
        Task<Outsourced> GetOutsourcedId(int id);
    }

    public class OutsourcedService : IOutsourcedService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public OutsourcedService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Outsourced> CreateAsync(OutsourcedModel model)
        {
            var outsou = _mapper.Map<Outsourced>(model);
            outsou.CreationDate = DateTime.Now;
            outsou.ChangeDate = DateTime.Now;
            outsou.CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId);

            _dbContext.Outsourseds.Add(outsou);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return outsou;
        }

        public async Task<Outsourced> UpdateOutsourcedAsync(int id, OutsourcedModel model)
        {
            var outsou = await _dbContext.Outsourseds.FindAsync(id);

            if (outsou != null)
            {
                outsou.TypePeople = model.TypePeople;
                outsou.Active = model.Active;
                outsou.Address = model.Address;
                outsou.CellPhone = model.CellPhone;
                outsou.EMail = model.EMail;
                outsou.Cpf = model.Cpf;
                outsou.Cnpj = model.Cnpj;
                outsou.FantasyName = model.FantasyName;
                outsou.CorporateName = model.CorporateName;
                outsou.Neighbourhood = model.Neighbourhood;
                outsou.Number = model.Number;
                outsou.Complement = model.Complement;
                outsou.City = model.City;
                outsou.State = model.State;
                outsou.Telephone = model.Telephone;
                outsou.ZipCode = model.ZipCode;
                outsou.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return outsou;
        }

        public async Task<Outsourced> GetOutsourcedId(int id)
        {
            return await _dbContext.Outsourseds.FindAsync(id);
        }

        public async Task<PageResponse<Outsourced>> GetOutsourcedsAsync(PageRequest<OutsourcedFilter, OutsourcedSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Outsourseds.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Outsourced> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Outsourced>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Outsourced> LoadOrder(PageRequest<OutsourcedFilter, OutsourcedSortingFields> pageRequest, IQueryable<Outsourced> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.Cnpj)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cnpj)
                    : dataQuery.OrderBy(x => x.Cnpj);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.CorporateName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.CorporateName)
                    : dataQuery.OrderBy(x => x.CorporateName);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.FantasyName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.FantasyName)
                    : dataQuery.OrderBy(x => x.FantasyName);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.Neighbourhood)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Neighbourhood)
                    : dataQuery.OrderBy(x => x.Neighbourhood);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.State)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.State)
                    : dataQuery.OrderBy(x => x.State);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.City)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.City)
                    : dataQuery.OrderBy(x => x.City);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.Cpf)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cpf)
                    : dataQuery.OrderBy(x => x.Cpf);
            }
            else if (pageRequest.OrderBy?.Field == OutsourcedSortingFields.TypePeople)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.TypePeople)
                    : dataQuery.OrderBy(x => x.TypePeople);
            }

            return dataQuery;
        }

        private static IQueryable<Outsourced> LoadFilterQuery(OutsourcedFilter filter, IQueryable<Outsourced> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null && filter.Id != 0)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.CorporateName))
            {
                filterQuery = filterQuery.Where(x => x.CorporateName.ToLower().Contains(filter.CorporateName.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.FantasyName))
            {
                filterQuery = filterQuery.Where(x => x.FantasyName.ToLower().Contains(filter.FantasyName.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.Cnpj))
            {
                filterQuery = filterQuery.Where(x => x.Cnpj.Contains(filter.Cnpj));
            }
            if (!string.IsNullOrEmpty(filter.City))
            {
                filterQuery = filterQuery.Where(x => x.City.ToLower().Contains(filter.City.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.Neighbourhood))
            {
                filterQuery = filterQuery.Where(x => x.Neighbourhood.ToLower().Contains(filter.Neighbourhood.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.State))
            {
                filterQuery = filterQuery.Where(x => x.State.ToLower().Contains(filter.State.ToLower()));
            }
            if (filter.Active != null)
            {
                filterQuery = filterQuery.Where(x => x.Active == filter.Active);
            }
            if (!string.IsNullOrEmpty(filter.Cpf))
            {
                filterQuery = filterQuery.Where(x => x.Cpf.ToLower().Contains(filter.Cpf.ToLower()));
            }
            if (filter.TypePeople != null)
            {
                filterQuery = filterQuery.Where(x => x.TypePeople == filter.TypePeople);
            }

            return filterQuery;
        }
    }
}
