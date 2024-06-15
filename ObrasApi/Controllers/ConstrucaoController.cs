using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionDomain.Enums;
using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.ConstructionDomain.Services;
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

        public ConstrucaoController(IConstructionService constructionService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.constructionService = constructionService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
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
            var model = this.mapper.Map<ConstructionModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConstructionInput input)
        {
            var model = this.mapper.Map<ConstructionModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = (int) user.CompanyId;

            var response = await constructionService.UpdateAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<ConstructionFilter, ConstructionSortingFields> pageRequest)
        {
            var response = await constructionService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

