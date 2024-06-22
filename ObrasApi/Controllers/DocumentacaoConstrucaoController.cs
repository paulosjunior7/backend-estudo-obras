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
using Obras.Business.ConstructionDocumentationDomain.Enums;
using Obras.Business.ConstructionDocumentationDomain.Models;
using Obras.Business.ConstructionDocumentationDomain.Request;
using Obras.Business.ConstructionDocumentationDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/Construcao/{construcaoId}/Documentacao")]
    public class DocumentacaoConstrucaoController : Controller
    {
        private readonly IConstructionDocumentationService documentationService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;
        private readonly IValidator<ConstructionDocumentationInput> _documentationValidator;

        public DocumentacaoConstrucaoController(IValidator<ConstructionDocumentationInput> documentationValidator, IConstructionDocumentationService documentationService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.documentationService = documentationService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
            this._documentationValidator = documentationValidator;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int construcaoId, int id)
        {
            var response = await documentationService.GetId(construcaoId, id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int construcaoId, [FromBody] ConstructionDocumentationInput input)
        {
            var validationResult = _documentationValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionDocumentationModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await documentationService.CreateAsync(model);
            model.Id = response.Id;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int construcaoId, int id, [FromBody] ConstructionDocumentationInput input)
        {
            var validationResult = _documentationValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var model = this.mapper.Map<ConstructionDocumentationModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.ConstructionId = construcaoId;

            var response = await documentationService.UpdateAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(int construcaoId, [FromBody] PageRequest<ConstructionDocumentationFilter, ConstructionDocumentationSortingFields> pageRequest)
        {
            pageRequest.Filter.ConstructionId = construcaoId;
            var response = await documentationService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

