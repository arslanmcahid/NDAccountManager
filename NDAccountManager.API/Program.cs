using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using NDAccountManager.API.Filters;
using NDAccountManager.API.Middlewares;
using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Repositories;
using NDAccountManager.Core.Services;
using NDAccountManager.Core.UnitOfWorks;
using NDAccountManager.Repository;
using NDAccountManager.Repository.Repositories;
using NDAccountManager.Repository.UnitOfWorks;
using NDAccountManager.Service.Mapping;
using NDAccountManager.Service.Services;
using NDAccountManager.Service.Validations;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// JSON Serialization 
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateFilterAttribute());
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    })
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<SharedAccountDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Azure AD 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Azure AD Token 
builder.Services.Configure<AzureADTokenOptionsDto>(builder.Configuration.GetSection("AzureAd"));

// Microsoft Graph
builder.Services.AddMicrosoftGraph(options =>
{
    options.Scopes = "User.Read GroupMember.Read.All";
});

// Swagger 
var azureAdSection = builder.Configuration.GetSection("AzureAd");
builder.Services.AddSwaggerGen(c =>
{
    // JWT Bearer Token 
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token"
    });

    // Azure AD OAuth2 
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{azureAdSection["Instance"]}{azureAdSection["TenantId"]}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"{azureAdSection["Instance"]}{azureAdSection["TenantId"]}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                    { $"api://{azureAdSection["ClientId"]}/.default", "Access the API" }
                }
            }
        }
    });

    // Security Requirements
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        // JWT Bearer Token
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>() 
        },
        // Azure AD OAuth2
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new List<string> { $"api://{azureAdSection["ClientId"]}/.default" } // Azure AD OAuth2 için gerekli scope'lar
        }
    });
});

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ISharedAccountRepository, SharedAccountRepository>();
builder.Services.AddScoped<ISharedAccountService, SharedAccountService>();

builder.Services.AddScoped<ITokenService, TokenService>();

// AutoMapper 
builder.Services.AddAutoMapper(typeof(MapProfile));

// SQL Server 
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId(azureAdSection["ClientId"]);
        c.OAuthClientSecret(azureAdSection["ClientSecret"]);
        c.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UserCustomException();  

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
