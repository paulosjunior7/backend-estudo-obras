using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Api.Validators;
using Obras.Business.ConstructionDomain.Enums;
using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.ConstructionDomain.Services;
using Obras.Business.EmployeeDomain.Request;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/[controller]")]
    public class ConstrucaoController : Controller
    {
        private readonly IConstructionService constructionService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;
        private readonly IValidator<ConstructionInput> _constructionValidator;
        private readonly IValidator<ConstructionAddressInput> _constructionAddressValidator;
        private readonly IValidator<ConstructionDateInput> _constructionDateValidator;

        public ConstrucaoController(
            IValidator<ConstructionInput> constructionValidator,
            IValidator<ConstructionAddressInput> constructionAddressValidator,
            IValidator<ConstructionDateInput> constructionDateValidator,
            IConstructionService constructionService,
            IMapper mapper,
            ObrasDBContext dBContext
        )
        {
            this.constructionService = constructionService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
            this._constructionValidator = constructionValidator;
            this._constructionAddressValidator = constructionAddressValidator;
            this._constructionDateValidator = constructionDateValidator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await constructionService.GetId(id);

            return Ok(this.mapper.Map<ConstructionModel>(response));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConstructionInput input)
        {
            var validationResult = _constructionValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = (int) user.CompanyId;

            var response = await constructionService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpPut("{id}/endereco")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] ConstructionAddressInput input)
        {
            var validationResult = _constructionAddressValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            var response = await constructionService.UpdateAddressAsync(id, user, input);
            var model = mapper.Map<ConstructionModel>(response);
            return Ok(model);
        }

        [HttpPut("{id}/dados")]
        public async Task<IActionResult> UpdateDate(int id, [FromBody] ConstructionDateInput input)
        {
            var validationResult = _constructionDateValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            var response = await constructionService.UpdateDateAsync(id, user, input);
            var model = mapper.Map<ConstructionModel>(response);
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<ConstructionFilter, ConstructionSortingFields> pageRequest)
        {
            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            pageRequest.Filter.CompanyId = user.CompanyId;

            var response = await constructionService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

