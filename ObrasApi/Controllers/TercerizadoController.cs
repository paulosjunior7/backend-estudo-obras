using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.OutsourcedDomain.Models;
using Obras.Business.OutsourcedDomain.Request;
using Obras.Business.OutsourcedDomain.Services;
using Obras.Business.OutsoursedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;


namespace Obras.Api.Controllers
{
    [Route("api/[controller]")]
    public class TercerizadoController : Controller
    {
        private readonly IOutsourcedService outsourcedService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public TercerizadoController(IOutsourcedService outsourcedService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.outsourcedService = outsourcedService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OutsourcedInput input)
        {
            var model = this.mapper.Map<OutsourcedModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await outsourcedService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await outsourcedService.GetOutsourcedId(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OutsourcedInput input)
        {
            var model = this.mapper.Map<OutsourcedModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await outsourcedService.UpdateOutsourcedAsync(id, model);
            model.Id = id;

            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<OutsourcedFilter, OutsourcedSortingFields> pageRequest)
        {
            var response = await outsourcedService.GetOutsourcedsAsync(pageRequest);

            return Ok(response);
        }
    }
}

