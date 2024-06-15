using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.SharedDomain.Models;
using Obras.Business.UnitDomain.Enums;
using Obras.Business.UnitDomain.Models;
using Obras.Business.UnitDomain.Request;
using Obras.Business.UnitDomain.Services;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/[controller]")]
    public class UnidadeController : Controller
    {
        private readonly IUnityService unityService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public UnidadeController(IUnityService unityService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.unityService = unityService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnityInput input)
        {
            var model = this.mapper.Map<UnityModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await unityService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            var response = await unityService.GetId((int) user.CompanyId!, id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UnityInput input)
        {
            var model = this.mapper.Map<UnityModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await unityService.UpdateAsync((int) user.CompanyId!, id, model);
            model.Id = id;

            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<UnityFilter, UnitySortingFields> pageRequest)
        {
            var response = await unityService.GetAsync(pageRequest);

            return Ok(response);
        }
    }
}

