using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obras.Business.CompanyDomain.Models;
using Obras.Business.CompanyDomain.Services;
using System.Threading.Tasks;
using static Obras.Business.SharedDomain.Helpers.Constants;

namespace Obras.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : Controller
    {
        private readonly ICompanyService companyService;

        public EmpresaController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] CompanyModel model)
        {
            var response = await companyService.CreateAsync(model);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await companyService.GetCompanyId(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyModel model)
        {
            var response = await companyService.UpdateCompanyAsync(id, model);

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await companyService.GetCompaniesAsync();

            return Ok(response);
        }
    }
}

