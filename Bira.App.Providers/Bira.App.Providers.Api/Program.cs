using Bira.App.Providers.Api.Configuration;
using Bira.App.Providers.Infra.Repositories.BaseContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var assembly = AppDomain.CurrentDomain.Load("Bira.App.Providers.Application");

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ConfigureServices
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddMediatR(assembly);

builder.Services.AddAutoMapper(assembly);

builder.Services.AddApiConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure
app.UseApiConfig(app.Environment);

app.MapControllers();

app.Run();
