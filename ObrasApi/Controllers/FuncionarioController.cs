using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.DocumentationDomain.Request;
using Obras.Business.EmployeeDomain.Enums;
using Obras.Business.EmployeeDomain.Models;
using Obras.Business.EmployeeDomain.Request;
using Obras.Business.EmployeeDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;


namespace Obras.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public FuncionarioController(IEmployeeService employeeService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeInput input)
        {
            var model = this.mapper.Map<EmployeeModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await employeeService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await employeeService.GetEmployeeId(id);

            return Ok(this.mapper.Map<EmployeeModel>(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeInput input)
        {
            var model = this.mapper.Map<EmployeeModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await employeeService.UpdateEmployeeAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<EmployeeFilter, EmployeeSortingFields> pageRequest)
        {
            var response = await employeeService.GetEmployeesAsync(pageRequest);

            return Ok(response);
        }
    }
}

