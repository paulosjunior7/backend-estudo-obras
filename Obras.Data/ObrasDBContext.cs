using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Obras.Data.Entities;
using System;

namespace Obras.Data
{
    public class ObrasDBContext : IdentityDbContext
    {
        public ObrasDBContext()
        {
        }

        public ObrasDBContext(DbContextOptions<ObrasDBContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}
