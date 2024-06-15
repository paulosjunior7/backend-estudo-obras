using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ResponsibilityDomain.Enums;
using Obras.Business.ResponsibilityDomain.Models;
using Obras.Business.ResponsibilityDomain.Request;
using Obras.Business.ResponsibilityDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoController : Controller
    {
        private readonly IResponsibilityService responsibilityService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public CargoController(IResponsibilityService responsibilityService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.responsibilityService = responsibilityService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResponsibilityInput input)
        {
            var model = this.mapper.Map<ResponsibilityModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await responsibilityService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await responsibilityService.GetResponsibilityId(id);

            return Ok(this.mapper.Map<ResponsibilityModel>(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ResponsibilityInput input)
        {
            var model = this.mapper.Map<ResponsibilityModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await responsibilityService.UpdateResponsibilityAsync(id, model);
            model.Id = id;
            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<ResponsibilityFilter, ResponsibilitySortingFields> pageRequest)
        {
            var response = await responsibilityService.GetResponsibilitiesAsync(pageRequest);

            return Ok(response);
        }
    }
}

