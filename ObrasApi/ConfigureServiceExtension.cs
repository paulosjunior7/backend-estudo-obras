namespace Obras.Api
{
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
    using Obras.Business.OutsourcedDomain.Services;
    using Obras.Business.PeopleDomain.Services;
    using Obras.Business.ProductDomain.Services;
    using Obras.Business.ProductProviderDomain.Services;
    using Obras.Business.ProviderDomain.Services;
    using Obras.Business.ResponsibilityDomain.Services;
    using Obras.Business.SharedDomain.Helpers;
    using Obras.Data;
    using Obras.Data.Entities;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Claims;
    using Obras.Business.ConstructionDomain.Services;
    using Obras.Business.ConstructionInvestorDomain.Services;
    using Obras.Business.ConstructionBatchDomain.Services;
    using Obras.Business.ConstructionHouseDomain.Services;
    using Obras.Business.UnitDomain.Services;
    using Obras.Business.GroupDomain.Services;
    using Obras.Business.ConstructionMaterialDomain.Services;
    using Obras.Business.ConstructionManpowerDomain.Services;
    using Obras.Business.ConstructionDocumentationDomain.Services;
    using Obras.Business.ConstructionExpenseDomain.Services;
    using Obras.Business.ConstructionAdvanceMoneyDomain.Services;
    using Microsoft.OpenApi.Models;

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
                options.AddPolicy("Engineer", policy => policy.RequireRole("Engineer"));
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
            services.AddTransient<IOutsourcedService, OutsourcedService>();
            services.AddTransient<IProductProviderService, ProductProviderService>();
            services.AddTransient<IConstructionService, ConstructionService>();
            services.AddTransient<IConstructionInvestorService, ConstructionInvestorService>();
            services.AddTransient<IConstructionBatchService, ConstructionBatchService>();
            services.AddTransient<IConstructionHouseService, ConstructionHouseService>();
            services.AddTransient<IUnityService, UnityService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IConstructionMaterialService, ConstructionMaterialService>();
            services.AddTransient<IConstructionManpowerService, ConstructionManpowerService>();
            services.AddTransient<IConstructionDocumentationService, ConstructionDocumentationService>();
            services.AddTransient<IConstructionExpenseService, ConstructionExpenseService>();
            services.AddTransient<IConstructionAdvanceMoneyService, ConstructionAdvanceMoneyService>();
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Obras API",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

            return services;
        }

    }
}
