using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.OutsourcedDomain.Models;
using Obras.Business.OutsourcedDomain.Request;
using Obras.Business.OutsourcedDomain.Services;
using Obras.Business.OutsoursedDomain.Enums;
using Obras.Business.ProviderDomain.Enums;
using Obras.Business.ProviderDomain.Models;
using Obras.Business.ProviderDomain.Request;
using Obras.Business.ProviderDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedorController : Controller
    {
        private readonly IProviderService providerService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public FornecedorController(IProviderService providerService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.providerService = providerService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProviderInput input)
        {
            var model = this.mapper.Map<ProviderModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await providerService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await providerService.GetProviderId(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProviderInput input)
        {
            var model = this.mapper.Map<ProviderModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await providerService.UpdateProviderAsync(id, model);
            model.Id = id;

            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<ProviderFilter, ProviderSortingFields> pageRequest)
        {
            var response = await providerService.GetProvidersAsync(pageRequest);

            return Ok(response);
        }
    }
}

