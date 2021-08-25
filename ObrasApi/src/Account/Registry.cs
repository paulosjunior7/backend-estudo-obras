using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObrasApi.src.Account.Api;
using ObrasApi.src.Account.BusinessRules.Handlers;
using ObrasApi.src.Account.BusinessRules.Validators;
using ObrasApi.src.Account.Database;
using ObrasApi.src.Account.Database.Repositories;
using ObrasApi.src.Company.Database;

namespace ObrasApi.src.Account
{
    public class Registry
    {
        public static void execute(IServiceCollection services, IConfiguration Configuration, IRequestExecutorBuilder query, IRequestExecutorBuilder mutation)
        {
            query.AddType<AccountQuery>();
            mutation.AddType<AccountMutation>();

            services
                // Validators
                .AddScoped<IAccountValidator, AccountValidator>()

                // Repositories
                .AddScoped<IAccountRepository, AccountRepository>()

                //Business Rules
                .AddScoped<IUpsertAccountHandler, UpsertAccountHandler>();
        }
    }
}