using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ExpenseDomain.Enums;
using Obras.Business.ExpenseDomain.Models;
using Obras.Business.ExpenseDomain.Request;
using Obras.Business.ExpenseDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

namespace Obras.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DespesaController : Controller
    {
        private readonly IExpenseService expenseService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public DespesaController(IExpenseService expenseService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.expenseService = expenseService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await expenseService.GetExpenseId(id);

            return Ok(this.mapper.Map<ExpenseModel>(response));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseInput input)
        {
            var model = this.mapper.Map<ExpenseModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await expenseService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseInput input)
        {
            var model = this.mapper.Map<ExpenseModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await expenseService.UpdateExpenseAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<ExpenseFilter, ExpenseSortingFields> pageRequest)
        {
            var response = await expenseService.GetExpensesAsync(pageRequest);

            return Ok(response);
        }
    }
}

