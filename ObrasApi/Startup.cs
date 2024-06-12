using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Obras.Api;
using Obras.Business.Mappings;
using Obras.Data;
using Obras.GraphQLModels.SharedDomain.Schemas;
using System.Text.Json.Serialization;

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

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();

            //IServiceCollection serviceCollection = services.AddDbContext<ObrasDBContext>(options => options
            //            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), optionsLifetime: ServiceLifetime.Singleton);
            //.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), optionsLifetime: ServiceLifetime.Singleton);
            services.AddDbContext<ObrasDBContext>(
                optionsAction: options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ObrasDBContext).Assembly.FullName)),
                contextLifetime: ServiceLifetime.Singleton);

            services.AddCustomIdentityAuth();

            services.AddCustomJWT(Configuration);

            services.AddCustomGraphQLAuth();

            services.AddCustomService();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            services.AddSwagger();

            services.AddCustomGraphQLServices();

            services.AddCustomGraphQLTypes();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ObrasDBContext dbContext)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ObrasDBContext>();
                context.Database.Migrate();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });

            //dbContext.EnsureDataSeeding();

            app.UseWebSockets();
        }
    }
}
