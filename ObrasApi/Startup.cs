using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Obras.Api;
using Obras.Api.Validators;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.EmployeeDomain.Request;
using Obras.Business.Mappings;
using Obras.Business.PeopleDomain.Request;
using Obras.Business.ProviderDomain.Request;
using Obras.Data;
using System;
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

            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new Obras.Data.Enums.TypePeopleConverter());
                        options.JsonSerializerOptions.Converters.Add(new Obras.Data.Enums.TypeExpenseConverter());
                        options.JsonSerializerOptions.Converters.Add(new Obras.Data.Enums.TypePhotoConverter());
                        options.JsonSerializerOptions.Converters.Add(new Obras.Data.Enums.StatusConstructionConverter());
                    });

            services.AddValidatorsFromAssemblyContaining<Startup>();

            //IServiceCollection serviceCollection = services.AddDbContext<ObrasDBContext>(options => options
            //            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), optionsLifetime: ServiceLifetime.Singleton);
            //.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), optionsLifetime: ServiceLifetime.Singleton);
            services.AddDbContext<ObrasDBContext>(
                optionsAction: options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ObrasDBContext).Assembly.FullName)),
                contextLifetime: ServiceLifetime.Singleton);

            services.AddCustomIdentityAuth();

            services.AddCustomJWT(Configuration);

            services.AddCustomService();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            services.AddSwagger();


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


            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //dbContext.EnsureDataSeeding();

            app.UseWebSockets();
        }
    }
}
