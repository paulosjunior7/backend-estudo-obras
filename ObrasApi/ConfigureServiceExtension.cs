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
    using Obras.Business.Services;
    using Obras.Data;
    using Obras.GraphQLModels;
    using Obras.GraphQLModels.Enums;
    using Obras.GraphQLModels.InputTypes;
    using Obras.GraphQLModels.Mutations;
    using Obras.GraphQLModels.Queries;
    using Obras.GraphQLModels.Schemas;
    using Obras.GraphQLModels.Types;
    using System;
    using System.IdentityModel.Tokens.Jwt;

    public static class ConfigureServiceExtension
    {
        public static void AddCustomIdentityAuth(this IServiceCollection services)
        {
            // Added Identity
            services.AddIdentity<Microsoft.AspNetCore.Identity.IdentityUser, IdentityRole>()
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                    ValidAudience = configuration.GetSection("JwtIssuerOptions:Audience").Value,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddTransient<ICompanyService, CompanyService>();
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
            .AddGraphTypes(typeof(CompanySchema));
        }

        public static void AddCustomGraphQLTypes(this IServiceCollection services)
        {
            services.AddSingleton<CompanyType>();

            services.AddSingleton<CompanySortingFieldsEnumType>();
            services.AddSingleton<SortingDirectionEnumType>();


            services.AddSingleton<CompanyByInputType>();
            services.AddSingleton<CompanyFilterByInputType>();
            services.AddSingleton<CompanyInputType>();

            services.AddSingleton<CompanyMutation>();
            services.AddSingleton<CompanyQuery>();
            services.AddSingleton<CompanySchema>();
        }

        public static void AddCustomGraphQLAuth(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.TryAddSingleton(_ =>
            {
                var authSettings = new AuthorizationSettings();
                /*
                authSettings.AddPolicy(
                    Constants.AuthPolicy.CustomerPolicy,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.Customer));

                authSettings.AddPolicy(
                    Constants.AuthPolicy.RestaurantPolicy,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.Restaurant));

                authSettings.AddPolicy(
                    Constants.AuthPolicy.AdminPolicy,
                    policy => policy.RequireClaim(ClaimTypes.Role, Constants.Roles.Customer, Constants.Roles.Restaurant, Constants.Roles.Admin));
                */
                return authSettings;
            });
        }
    }
}
