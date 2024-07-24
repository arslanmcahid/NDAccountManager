using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using NDAccountManager.API.Filters;
using NDAccountManager.API.Middlewares;
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

//CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
//CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateFilterAttribute()); 
})
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AccountDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddMicrosoftGraph(options =>
{
    options.Scopes = "User.Read GroupMember.Read.All";
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//scopes
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//mapper
builder.Services.AddAutoMapper(typeof(MapProfile));

// SQL Server Connection
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
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UserCustomException();// ozellestirilmis hata yakalama

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
