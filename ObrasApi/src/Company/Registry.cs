using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObrasApi.src.Company.Api;
using ObrasApi.src.Company.BusinessRules.Handlers;
using ObrasApi.src.Company.BusinessRules.Validators;
using ObrasApi.src.Company.Database;
using ObrasApi.src.Company.Database.Repositories;

namespace ObrasApi.src.Company
{
    public class Registry
    {
        public static void execute(IServiceCollection services, IConfiguration Configuration, IRequestExecutorBuilder query, IRequestExecutorBuilder mutation)
        {
            query.AddType<CompanyQuery>();
            mutation.AddType<CompanyMutation>();

            services
                // Validators
                .AddScoped<ICompanyValidator, CompanyValidator>()

                // Repositories
                .AddScoped<ICompanyRepository, CompanyRepository>()

                //Business Rules
                .AddScoped<IUpsertCompanyHandler, UpsertCompanyHandler>()
                .AddScoped<IGetByIdCompanyHandler, GetByIdCompanyHandler>()
                .AddScoped<IGetAllCompaniesHandler, GetAllCompaniesHandler>();
        }
    }
}