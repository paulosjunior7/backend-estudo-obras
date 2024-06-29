using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionMaterialDomain.Enums;
using Obras.Business.ConstructionMaterialDomain.Models;
using Obras.Business.ConstructionMaterialDomain.Request;
using Obras.Business.ConstructionMaterialDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/Construcao/{construcaoId}/Material")]
    public class MaterialConstrucaoController : Controller
    {
        private readonly IConstructionMaterialService materialService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;
        private readonly IValidator<ConstructionMaterialInput> _materialValidator;

        public MaterialConstrucaoController(IValidator<ConstructionMaterialInput> materialValidator, IConstructionMaterialService materialService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.materialService = materialService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
            this._materialValidator = materialValidator;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int construcaoId, int id)
        {
            var response = await materialService.GetId(construcaoId, id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int construcaoId, [FromBody] ConstructionMaterialInput input)
        {
            var validationResult = _materialValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionMaterialModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await materialService.CreateAsync(model);
            model.Id = response.Id;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int construcaoId, int id, [FromBody] ConstructionMaterialInput input)
        {
            var validationResult = _materialValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionMaterialModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await materialService.UpdateAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(int construcaoId, [FromBody] PageRequest<ConstructionMaterialFilter, ConstructionMaterialSortingFields> pageRequest)
        {
            pageRequest.Filter.ConstructionId = construcaoId;
            var response = await materialService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

