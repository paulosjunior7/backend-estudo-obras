using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionInvestorDomain.Enums;
using Obras.Business.ConstructionInvestorDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Request;
using Obras.Business.ConstructionInvestorDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/Construcao/{construcaoId}/Investidor")]
    public class InvestidorConstrucaoController : Controller
    {
        private readonly IConstructionInvestorService investorService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;
        private readonly IValidator<ConstructionInvestorInput> _investorValidator;

        public InvestidorConstrucaoController(IValidator<ConstructionInvestorInput> investorValidator, IConstructionInvestorService investorService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.investorService = investorService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
            this._investorValidator = investorValidator;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int construcaoId, int id)
        {
            var response = await investorService.GetId(construcaoId, id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int construcaoId, [FromBody] ConstructionInvestorInput input)
        {
            var validationResult = _investorValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionInvestorModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await investorService.CreateAsync(model);
            model.Id = response.Id;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int construcaoId, int id, [FromBody] ConstructionInvestorInput input)
        {
            var validationResult = _investorValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionInvestorModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await investorService.UpdateAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(int construcaoId, [FromBody] PageRequest<ConstructionInvestorFilter, ConstructionInvestorSortingFields> pageRequest)
        {
            pageRequest.Filter.ConstructionId = construcaoId;
            var response = await investorService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

