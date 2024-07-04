using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.EmployeeDomain.Enums;
using Obras.Business.EmployeeDomain.Models;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.EmployeeDomain.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(EmployeeModel model);
        Task<Employee> UpdateEmployeeAsync(int id, EmployeeModel model);
        Task<PageResponse<EmployeeModel>> GetEmployeesAsync(PageRequest<EmployeeFilter, EmployeeSortingFields> pageRequest);
        Task<Employee> GetEmployeeId(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Employee> CreateAsync(EmployeeModel model)
        {
            var emp = _mapper.Map<Employee>(model);
            emp.CreationDate = DateTime.Now;
            emp.ChangeDate = DateTime.Now;
            emp.CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId);

            // Certifique-se de que a responsabilidade existe no banco de dados
            var responsibility = await _dbContext.Responsibilities
                .SingleOrDefaultAsync(r => r.Id == emp.ResponsibilityId);

            if (responsibility == null)
            {
                throw new Exception($"Responsibility with Id {emp.ResponsibilityId} does not exist.");
            }

            // Anexe a responsabilidade existente ao contexto sem marcá-la como modificada
            _dbContext.Entry(responsibility).State = EntityState.Unchanged;

            _dbContext.Employees.Add(emp);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return emp;
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, EmployeeModel model)
        {
            var emp = await _dbContext.Employees.FindAsync(id);

            if (emp != null)
            {
                emp.Active = model.Active;
                emp.Address = model.Address;
                emp.CellPhone = model.CellPhone;
                emp.EMail = model.EMail;
                emp.Cpf = model.Cpf;
                emp.Cnpj = model.Cnpj;
                emp.Outsourced = model.Outsourced;
                emp.Employed = model.Employed;
                emp.ResponsibilityId = model.ResponsibilityId;
                emp.Neighbourhood = model.Neighbourhood;
                emp.Number = model.Number;
                emp.State = model.State;
                emp.Complement = model.Complement;
                emp.City = model.City;
                emp.Telephone = model.Telephone;
                emp.ZipCode = model.ZipCode;
                emp.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return emp;
        }

        public async Task<Employee> GetEmployeeId(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task<PageResponse<EmployeeModel>> GetEmployeesAsync(PageRequest<EmployeeFilter, EmployeeSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Employees.Include(x => x.Responsibility).Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<Employee> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            var itens = this._mapper.Map<List<EmployeeModel>>(nodes);

            return new PageResponse<EmployeeModel>
            {
                Nodes = itens,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Employee> LoadOrder(PageRequest<EmployeeFilter, EmployeeSortingFields> pageRequest, IQueryable<Employee> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.EmployeeSortingFields.Cpf)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cpf)
                    : dataQuery.OrderBy(x => x.Cpf);
            }
            else if (pageRequest.OrderBy?.Field == Enums.EmployeeSortingFields.Name)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Name)
                    : dataQuery.OrderBy(x => x.Name);
            }
            else if (pageRequest.OrderBy?.Field == Enums.EmployeeSortingFields.Responsibility)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Responsibility.Description)
                    : dataQuery.OrderBy(x => x.Responsibility.Description);
            }
            else if (pageRequest.OrderBy?.Field == Enums.EmployeeSortingFields.Neighbourhood)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Neighbourhood)
                    : dataQuery.OrderBy(x => x.Neighbourhood);
            }
            else if (pageRequest.OrderBy?.Field == Enums.EmployeeSortingFields.State)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.State)
                    : dataQuery.OrderBy(x => x.State);
            }
            else if (pageRequest.OrderBy?.Field == Enums.EmployeeSortingFields.City)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.City)
                    : dataQuery.OrderBy(x => x.City);
            }
            else if (pageRequest.OrderBy?.Field == Enums.EmployeeSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }

            return dataQuery;
        }

        private static IQueryable<Employee> LoadFilterQuery(EmployeeFilter filter, IQueryable<Employee> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                filterQuery = filterQuery.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            }
            if (filter.ResponsibilityId != null)
            {
                filterQuery = filterQuery.Where(x => x.ResponsibilityId == filter.ResponsibilityId);
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

            return filterQuery;
        }
    }
}
