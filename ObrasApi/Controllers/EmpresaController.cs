using Microsoft.AspNetCore.Mvc;
using Obras.Business.CompanyDomain.Models;
using Obras.Business.CompanyDomain.Services;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> Create([FromBody] CompanyModel model)
        {
            var response = await companyService.CreateAsync(model);

            return Ok(response);
        }
    }
}

