using HousingAnalysis.ApiServer.Data;
using HousingAnalysis.ApiServer.Extension;
using HousingAnalysis.ApiServer.Extension.Interfaces;
using HousingAnalysis.ApiServer.Repository;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using HousingAnalysis.ApiServer.Services;
using HousingAnalysis.ApiServer.Services.Interfaces;
using HousingAnalysis.ApiServer.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Packt.Shared; // метод расширения AddNorthwindContext
using static System.Console;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IJwtProvider,JwtProvider>();

builder.Services.AddScoped<IHousePropertyRepository, HouseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserService, UserService>();


// Настройка JWT и аутентификация
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]))
    };

    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["tasty-cookies"];
            return Task.CompletedTask;
        }
    };
}).AddCookie();
builder.Services.AddCors();
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddHouseDBContext(connectionString);
builder.Services.AddControllers(options =>
    {
        WriteLine("Default output formatters:");
        foreach (IOutputFormatter formatter in options.OutputFormatters)
        {
            OutputFormatter? mediaFormatter = formatter as OutputFormatter;
            if (mediaFormatter == null)
            {
                WriteLine($" {formatter.GetType().Name}");
            }
            else // класс форматера вывода с поддерживаемыми медиаформатами
            {
                WriteLine(" {0}, Media types: {1}",
                    arg0: mediaFormatter.GetType().Name,
                    arg1: string.Join(", ",
                        mediaFormatter.SupportedMediaTypes));
            }
        }
    })
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();