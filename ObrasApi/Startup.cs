using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObrasApi.src.Shared.Database;
using ObrasApi.src.Shared.GraphQL;
using ObrasApi.src.Shared.GraphQL.Account;
using ObrasApi.src.Shared.GraphQL.Company;

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
            services.AddPooledDbContextFactory<AppDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<CompanyType>()
                .AddType<AccountType>()
                .AddProjections()
                .AddMutationType<Mutation>()
                .AddFiltering()
                .AddSorting();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            }, "/graphql-ui");
        }
    }
}
