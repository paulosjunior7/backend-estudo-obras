using AutoMapper;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.BrandDomain.Enums;
using Obras.Business.BrandDomain.Models;
using Obras.Business.BrandDomain.Request;
using Obras.Business.BrandDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : Controller
    {
        private readonly IBrandService brandService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public MarcaController(IBrandService brandService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.brandService = brandService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandInput input)
        {
            var model = this.mapper.Map<BrandModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await brandService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await brandService.GetBrandId(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BrandInput input)
        {
            var model = this.mapper.Map<BrandModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new ExecutionError("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await brandService.UpdateBrandAsync(id, model);
            model.Id = id;

            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<BrandFilter, BrandSortingFields> pageRequest)
        {
            var response = await brandService.GetBrandsAsync(pageRequest);

            return Ok(response);
        }
    }
}

