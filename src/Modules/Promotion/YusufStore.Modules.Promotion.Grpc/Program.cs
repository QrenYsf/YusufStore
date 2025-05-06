using Promotion.Grpc.Data;
using Promotion.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<PromotionContext>(opts =>
        opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

app.UseMigration();
app.MapGrpcService<PromotionService>();
app.MapGet("/", () => "Communication with gRPC");

app.Run();
