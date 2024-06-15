using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ProductDomain.Enums;
using Obras.Business.ProductDomain.Models;
using Obras.Business.ProductDomain.Request;
using Obras.Business.ProductDomain.Services;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProductService produtcService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public ProdutoController(IProductService produtcService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.produtcService = produtcService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductInput input)
        {
            var model = this.mapper.Map<ProductModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.RegistrationUserId = user.Id;
            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await produtcService.CreateAsync(model);
            model.Id = response.Id;

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await produtcService.GetProductId(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductInput input)
        {
            var model = this.mapper.Map<ProductModel>(input);

            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault().Value;
            if (userId == null) return Unauthorized();

            var user = await userRepository.FindAsync(userId);
            if (user == null || user.CompanyId == null)
                throw new Exception("Usuário não exite ou não possui empresa vinculada!");

            model.ChangeUserId = user.Id;
            model.CompanyId = user.CompanyId;

            var response = await produtcService.UpdateProductAsync(id, model);
            model.Id = id;

            return Ok(model);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PageRequest<ProductFilter, ProductSortingFields> pageRequest)
        {
            var response = await produtcService.GetProductsAsync(pageRequest);

            return Ok(response);
        }
    }
}

