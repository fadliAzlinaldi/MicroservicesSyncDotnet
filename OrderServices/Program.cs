using Microsoft.EntityFrameworkCore;
using OrderServices.Data;
using OrderServices.Models;
using OrderServices.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// service automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// service interface product repo
builder.Services.AddScoped<IProductRepo, ProductRepo>();

// service interface wallet repo
builder.Services.AddScoped<IWalletRepo, WalletRepo>();

// service interface order repo
builder.Services.AddScoped<IOrderRepo, OrderRepo>();

// service data client product 
builder.Services.AddHttpClient<IProductDataClient, ProductDataClient>();

// service data client wallet
builder.Services.AddHttpClient<IWalletDataClient, WalletDataClient>();

// seed db service
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
