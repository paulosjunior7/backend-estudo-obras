using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.PeopleDomain.Enums;
using Obras.Business.PhotoDomain.Enums;
using Obras.Business.PhotoDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.PhotoDomain.Services
{
    public interface IPhotoService
    {
        Task<Photo> CreateAsync(PhotoModel model);
        Task<Photo> UpdateAsync(int id, PhotoModel model);
        Task<PageResponse<Photo>> GetAsync(PageRequest<PhotoFilter, PhotoSortingFields> pageRequest);
        Task<Photo> GetId(int id);
    }

    public class PhotoService : IPhotoService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public PhotoService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Photo> CreateAsync(PhotoModel model)
        {
            var pho = _mapper.Map<Photo>(model);
            pho.CreationDate = DateTime.Now;
            pho.ChangeDate = DateTime.Now;

            _dbContext.Photos.Add(pho);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return pho;
        }

        public async Task<Photo> UpdateAsync(int id, PhotoModel model)
        {
            var peo = await _dbContext.Photos.FindAsync(id);

            if (peo != null)
            {
                peo.TypePhoto = model.TypePhoto;
                peo.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return peo;
        }

        public async Task<Photo> GetId(int id)
        {
            return await _dbContext.Photos.FindAsync(id);
        }

        public async Task<PageResponse<Photo>> GetAsync(PageRequest<PhotoFilter, PhotoSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Photos.Where(x => x.Id >= 0);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Photo> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            return new PageResponse<Photo>
            {
                Nodes = nodes,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Photo> LoadOrder(PageRequest<PhotoFilter, PhotoSortingFields> pageRequest, IQueryable<Photo> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.PhotoSortingFields.Id)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Id)
                    : dataQuery.OrderBy(x => x.Id);
            }

            return dataQuery;
        }

        private static IQueryable<Photo> LoadFilterQuery(PhotoFilter filter, IQueryable<Photo> filterQuery)
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
                filterQuery = filterQuery.Where(x => x.ConstrucationId == filter.ConstructionId);
            }
            if (filter.TypePhoto != null)
            {
                filterQuery = filterQuery.Where(x => x.TypePhoto == filter.TypePhoto);
            }

            return filterQuery;
        }
    }
}
