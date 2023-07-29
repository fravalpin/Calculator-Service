using CalculatorService.Server.WebAPI;
using CalculatorService.Server.Application;
using CalculatorService.Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationsServices();
builder.Services.AddInfrastructureServices(builder.Logging, builder.Host);
builder.Services.AddAPIServices();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddAPIApplications();


app.Run();
