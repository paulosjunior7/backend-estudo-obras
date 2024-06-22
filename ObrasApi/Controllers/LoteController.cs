using System;
using System.Collections.Generic;
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
using Obras.Business.ConstructionBatchDomain.Enums;
using Obras.Business.ConstructionBatchDomain.Models;
using Obras.Business.ConstructionBatchDomain.Request;
using Obras.Business.ConstructionBatchDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/Construcao/{construcaoId}/[controller]")]
    public class LoteController : Controller
    {
        private readonly IConstructionBatchService batchService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;
        private readonly IValidator<ConstructionBatchInput> _batchValidator;

        public LoteController(IValidator<ConstructionBatchInput> batchValidator, IConstructionBatchService batchService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.batchService = batchService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
            this._batchValidator = batchValidator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int construcaoId, int id)
        {
            var response = await batchService.GetId(construcaoId, id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<ConstructionBatchModel>(response));
        }

        [HttpPost]
        public async Task<IActionResult> Create(int construcaoId, [FromBody] ConstructionBatchInput input)
        {
            var validationResult = _batchValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionBatchModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await batchService.CreateAsync(model);
            model.Id = response.Id;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int construcaoId, int id, [FromBody] ConstructionBatchInput input)
        {
            var validationResult = _batchValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionBatchModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await batchService.UpdateAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(int construcaoId, [FromBody] PageRequest<ConstructionBatchFilter, ConstructionBatchSortingFields> pageRequest)
        {
            pageRequest.Filter.ConstructionId = construcaoId;
            var response = await batchService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

