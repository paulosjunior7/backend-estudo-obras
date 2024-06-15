using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.DocumentationDomain.Enums;
using Obras.Business.DocumentationDomain.Models;
using Obras.Business.DocumentationDomain.Request;
using Obras.Business.DocumentationDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

namespace Obras.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentacaoController : Controller
    {
        private readonly IDocumentationService documentationService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public DocumentacaoController(IDocumentationService documentationService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.documentationService = documentationService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DocumentationInput input)
        {
            var model = this.mapper.Map<DocumentationModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await documentationService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await documentationService.GetDocumentationId(id);

            return Ok(this.mapper.Map<DocumentationModel>(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DocumentationInput input)
        {
            var model = this.mapper.Map<DocumentationModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await documentationService.UpdateDocumentationAsync(id, model);
            model.Id = id;

            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<DocumentationFilter, DocumentationSortingFields> pageRequest)
        {
            var response = await documentationService.GetDocumentationsAsync(pageRequest);

            return Ok(response);
        }
    }
}

