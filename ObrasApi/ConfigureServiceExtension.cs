namespace Obras.Api
{
    using GraphQL;
    using GraphQL.Authorization;
    using GraphQL.Server;
    using GraphQL.Validation;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Tokens;
    using Obras.Business.BrandDomain.Services;
    using Obras.Business.CompanyDomain.Services;
    using Obras.Business.DocumentationDomain.Services;
    using Obras.Business.EmployeeDomain.Services;
    using Obras.Business.ExpenseDomain.Services;
    using Obras.Business.PeopleDomain.Services;
    using Obras.Business.ProductDomain.Services;
    using Obras.Business.ProviderDomain.Services;
    using Obras.Business.ResponsibilityDomain.Services;
    using Obras.Business.SharedDomain.Helpers;
    using Obras.Data;
    using Obras.Data.Entities;
    using Obras.GraphQLModels;
    using Obras.GraphQLModels.BrandDomain.Enums;
    using Obras.GraphQLModels.BrandDomain.InputTypes;
    using Obras.GraphQLModels.BrandDomain.Mutations;
    using Obras.GraphQLModels.BrandDomain.Queries;
    using Obras.GraphQLModels.BrandDomain.Types;
    using Obras.GraphQLModels.BrandDomainInputTypes;
    using Obras.GraphQLModels.CompanyDomain.Enums;
    using Obras.GraphQLModels.CompanyDomain.InputTypes;
    using Obras.GraphQLModels.CompanyDomain.Mutations;
    using Obras.GraphQLModels.CompanyDomain.Queries;
    using Obras.GraphQLModels.CompanyDomain.Types;
    using Obras.GraphQLModels.DocumentationDomain.Enums;
    using Obras.GraphQLModels.DocumentationDomain.InputTypes;
    using Obras.GraphQLModels.DocumentationDomain.Mutations;
    using Obras.GraphQLModels.DocumentationDomain.Queries;
    using Obras.GraphQLModels.DocumentationDomain.Types;
    using Obras.GraphQLModels.EmployeeDomain.Enums;
    using Obras.GraphQLModels.EmployeeDomain.InputTypes;
    using Obras.GraphQLModels.EmployeeDomain.Mutations;
    using Obras.GraphQLModels.EmployeeDomain.Queries;
    using Obras.GraphQLModels.EmployeeDomain.Types;
    using Obras.GraphQLModels.ExpenseDomain.Enums;
    using Obras.GraphQLModels.ExpenseDomain.InputTypes;
    using Obras.GraphQLModels.ExpenseDomain.Mutations;
    using Obras.GraphQLModels.ExpenseDomain.Queries;
    using Obras.GraphQLModels.ExpenseDomain.Types;
    using Obras.GraphQLModels.PeopleDomain.Enums;
    using Obras.GraphQLModels.PeopleDomain.InputTypes;
    using Obras.GraphQLModels.PeopleDomain.Mutations;
    using Obras.GraphQLModels.PeopleDomain.Types;
    using Obras.GraphQLModels.PeopleDomainQueries;
    using Obras.GraphQLModels.ProductDomain.Enums;
    using Obras.GraphQLModels.ProductDomain.InputTypes;
    using Obras.GraphQLModels.ProductDomain.Mutations;
    using Obras.GraphQLModels.ProductDomain.Queries;
    using Obras.GraphQLModels.ProductDomain.Types;
    using Obras.GraphQLModels.ProviderDomain.Enums;
    using Obras.GraphQLModels.ProviderDomain.InputTypes;
    using Obras.GraphQLModels.ProviderDomain.Mutations;
    using Obras.GraphQLModels.ProviderDomain.Queries;
    using Obras.GraphQLModels.ProviderDomain.Types;
    using Obras.GraphQLModels.ResponsibilityDomain.Enums;
    using Obras.GraphQLModels.ResponsibilityDomain.InputTypes;
    using Obras.GraphQLModels.ResponsibilityDomain.Mutations;
    using Obras.GraphQLModels.ResponsibilityDomain.Queries;
    using Obras.GraphQLModels.ResponsibilityDomain.Types;
    using Obras.GraphQLModels.SharedDomain.Enums;
    using Obras.GraphQLModels.SharedDomain.Mutations;
    using Obras.GraphQLModels.SharedDomain.Queries;
    using Obras.GraphQLModels.SharedDomain.Schemas;
    using Obras.GraphQLModels.SharedDomain.Types;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Claims;

    public static class ConfigureServiceExtension
    {
        public static void AddCustomIdentityAuth(this IServiceCollection services)
        {
            // Added Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ObrasDBContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password configs
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // ApplicationUser settings
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-_";
            });
        }

        public static void AddCustomJWT(this IServiceCollection services, IConfiguration configuration)
        {
            // Added JWT Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(configuration.GetSection("JwtIssuerOptions:SecretKey").Value));


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                    ValidAudience = configuration.GetSection("JwtIssuerOptions:Audience").Value,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IDocumentationService, DocumentationService>();
            services.AddTransient<IResponsibilityService, ResponsibilityService>();
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }

        public static void AddCustomGraphQLServices(this IServiceCollection services)
        {
            // GraphQL services
            services.AddScoped<IServiceProvider>(c => new FuncServiceProvider(type => c.GetRequiredService(type)));
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.UnhandledExceptionDelegate = context =>
                {
                    Console.WriteLine("Error: " + context.OriginalException.Message);
                };
            })
            .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User })
            .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes(typeof(ObrasSchema));
        }

        private static void AddCompany(IServiceCollection services)
        {
            services.AddSingleton<CompanyType>();
            services.AddSingleton<CompanySortingFieldsEnumType>();
            services.AddSingleton<CompanyByInputType>();
            services.AddSingleton<CompanyFilterByInputType>();
            services.AddSingleton<CompanyInputType>();
            services.AddSingleton<CompanyMutation>();
            services.AddSingleton<CompanyQuery>();
        }

        private static void AddProduct(IServiceCollection services)
        {
            services.AddSingleton<ProductType>();
            services.AddSingleton<ProductSortingFieldsEnumType>();
            services.AddSingleton<ProductByInputType>();
            services.AddSingleton<ProductFilterByInputType>();
            services.AddSingleton<ProductInputType>();
            services.AddSingleton<ProductMutation>();
            services.AddSingleton<ProductQuery>();
        }

        private static void AddProvider(IServiceCollection services)
        {
            services.AddSingleton<ProviderType>();
            services.AddSingleton<ProviderSortingFieldsEnumType>();
            services.AddSingleton<ProviderByInputType>();
            services.AddSingleton<ProviderFilterByInputType>();
            services.AddSingleton<ProviderInputType>();
            services.AddSingleton<ProviderMutation>();
            services.AddSingleton<ProviderQuery>();
        }

        private static void AddBrand(IServiceCollection services)
        {
            services.AddSingleton<BrandType>();
            services.AddSingleton<BrandSortingFieldsEnumType>();
            services.AddSingleton<BrandByInputType>();
            services.AddSingleton<BrandFilterByInputType>();
            services.AddSingleton<BrandInputType>();
            services.AddSingleton<BrandMutation>();
            services.AddSingleton<BrandQuery>();
        }

        private static void AddDocumentation(IServiceCollection services)
        {
            services.AddSingleton<DocumentationType>();
            services.AddSingleton<DocumentationSortingFieldsEnumType>();
            services.AddSingleton<DocumentationByInputType>();
            services.AddSingleton<DocumentationFilterByInputType>();
            services.AddSingleton<DocumentationInputType>();
            services.AddSingleton<DocumentationMutation>();
            services.AddSingleton<DocumentationQuery>();
        }

        private static void AddResponsibility(IServiceCollection services)
        {
            services.AddSingleton<ResponsibilityType>();
            services.AddSingleton<ResponsibilitySortingFieldsEnumType>();
            services.AddSingleton<ResponsibilityByInputType>();
            services.AddSingleton<ResponsibilityFilterByInputType>();
            services.AddSingleton<ResponsibilityInputType>();
            services.AddSingleton<ResponsibilityMutation>();
            services.AddSingleton<ResponsibilityQuery>();
        }

        private static void AddExpense(IServiceCollection services)
        {
            services.AddSingleton<ExpenseType>();
            services.AddSingleton<ExpenseSortingFieldsEnumType>();
            services.AddSingleton<TypeExpenseEnumType>();
            services.AddSingleton<ExpenseByInputType>();
            services.AddSingleton<ExpenseFilterByInputType>();
            services.AddSingleton<ExpenseInputType>();
            services.AddSingleton<ExpenseMutation>();
            services.AddSingleton<ExpenseQuery>();
        }

        private static void AddPeople(IServiceCollection services)
        {
            services.AddSingleton<PeopleType>();
            services.AddSingleton<PeopleSortingFieldsEnumType>();
            services.AddSingleton<TypePeopleEnumType>();
            services.AddSingleton<PeopleByInputType>();
            services.AddSingleton<PeopleFilterByInputType>();
            services.AddSingleton<PeopleInputType>();
            services.AddSingleton<PeopleMutation>();
            services.AddSingleton<PeopleQuery>();
        }

        private static void AddEmployee(IServiceCollection services)
        {
            services.AddSingleton<EmployeeType>();
            services.AddSingleton<EmployeeSortingFieldsEnumType>();
            services.AddSingleton<EmployeeByInputType>();
            services.AddSingleton<EmployeeFilterByInputType>();
            services.AddSingleton<EmployeeInputType>();
            services.AddSingleton<EmployeeMutation>();
            services.AddSingleton<EmployeeQuery>();
        }

        public static void AddCustomGraphQLTypes(this IServiceCollection services)
        {
            AddCompany(services);

            services.AddSingleton<UserType>();
            
            AddProduct(services);
            AddProvider(services);
            AddBrand(services);
            AddDocumentation(services);
            AddResponsibility(services);
            AddExpense(services);
            AddPeople(services);
            AddEmployee(services);

            services.AddSingleton<SortingDirectionEnumType>();

            services.AddSingleton<ObrasQuery>();
            services.AddSingleton<ObrasMutation>();
            services.AddSingleton<ObrasSchema>();
        }

        public static void AddCustomGraphQLAuth(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.TryAddSingleton(_ =>
            {
                var authSettings = new AuthorizationSettings();

                authSettings.AddPolicy("LoggedIn", p => p.RequireAuthenticatedUser());

                authSettings.AddPolicy(
                    Constants.AuthPolicy.CustomerPolicy,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.Customer));

                authSettings.AddPolicy(
                    Constants.AuthPolicy.EngineerPolicy,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.Engineer));

                authSettings.AddPolicy(
                    Constants.AuthPolicy.AdminPolicy,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.Customer, Constants.Roles.Engineer, Constants.Roles.Admin));
                
                return authSettings;
            });
        }
    }
}
