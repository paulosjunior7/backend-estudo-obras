using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionAdvanceMoneyDomain.Enums;
using Obras.Business.ConstructionAdvanceMoneyDomain.Models;
using Obras.Business.ConstructionAdvanceMoneyDomain.Request;
using Obras.Business.ConstructionAdvanceMoneyDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/Construcao/{construcaoId}/[controller]")]
    public class AdiantarDinheiroController : Controller
    {
        private readonly IConstructionAdvanceMoneyService  advanceMoneyService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;
        private readonly IValidator<ConstructionAdvanceMoneyInput> _advanceMoneyValidator;

        public AdiantarDinheiroController(IValidator<ConstructionAdvanceMoneyInput> advanceMoneyValidator, IConstructionAdvanceMoneyService advanceMoneyService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.advanceMoneyService = advanceMoneyService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
            this._advanceMoneyValidator = advanceMoneyValidator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int construcaoId, int id)
        {
            var response = await advanceMoneyService.GetId(construcaoId, id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<ConstructionAdvanceMoneyModel>( response));
        }

        [HttpPost]
        public async Task<IActionResult> Create(int construcaoId, [FromBody] ConstructionAdvanceMoneyInput input)
        {
            var validationResult = _advanceMoneyValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionAdvanceMoneyModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await advanceMoneyService.CreateAsync(model);
            model.Id = response.Id;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int construcaoId, int id, [FromBody] ConstructionAdvanceMoneyInput input)
        {
            var validationResult = _advanceMoneyValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionAdvanceMoneyModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await advanceMoneyService.UpdateAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(int construcaoId, [FromBody] PageRequest<ConstructionAdvanceMoneyFilter, ConstructionAdvanceMoneySortingFields> pageRequest)
        {
            pageRequest.Filter.ConstructionId = construcaoId;
            var response = await advanceMoneyService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

