using GraphQL.Server;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Obras.Api;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.Schemas;

namespace ObrasApi
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();

            //IServiceCollection serviceCollection = services.AddDbContext<ObrasDBContext>(options => options
            //            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), optionsLifetime: ServiceLifetime.Singleton);
            //.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), optionsLifetime: ServiceLifetime.Singleton);

            services.AddDbContext<ObrasDBContext>(
                optionsAction: options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                contextLifetime: ServiceLifetime.Singleton);

            services.AddSingleton<ObrasDBContext>();

            services.AddCustomIdentityAuth();

            services.AddCustomJWT(Configuration);

            services.AddCustomGraphQLAuth();

            services.AddCustomService();

            services.AddCustomGraphQLServices();

            services.AddCustomGraphQLTypes();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ObrasDBContext dbContext)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //dbContext.EnsureDataSeeding();

            app.UseWebSockets();

            app.UseGraphQL<ObrasSchema>();

            app.UseGraphQLWebSockets<ObrasSchema>();

            app.UseGraphQLPlayground();
        }
    }
}
