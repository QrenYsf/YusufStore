using YusufStore.Modules.Purchasing.API;
using YusufStore.Modules.Purchasing.Application;
using YusufStore.Modules.Purchasing.Infrastructure;
using YusufStore.Modules.Purchasing.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
