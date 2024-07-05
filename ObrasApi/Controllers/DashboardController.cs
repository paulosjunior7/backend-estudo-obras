using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obras.Business.DashboardDomain.Services;
using Obras.Data;
using Obras.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obras.Api.Controllers
{
    [Route("api/Construcao/{construcaoId}/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly IMapper mapper;
        private readonly DbSet<User> userRepository;

        public DashboardController(IDashboardService dashboardService, IMapper mapper, ObrasDBContext dBContext)
        {
            this.dashboardService = dashboardService;
            this.mapper = mapper;
            this.userRepository = dBContext.User;
        }

        // GET: api/values
        [HttpGet("total-despesa")]
        public async Task<IActionResult> Get(int? construcaoId)
        {
            if (construcaoId == null)
            {
                return Ok();
            }
            return Ok(await this.dashboardService.GetTotalExpense((int) construcaoId));
        }

        
    }
}

