using Microsoft.EntityFrameworkCore;
using WalletServices.Data;
using WalletServices.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts =>
    opts.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// service automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// service interface product repo
builder.Services.AddScoped<IWalletRepo, WalletRepo>();

// service interface order data client
builder.Services.AddScoped<IOrderDataClient, OrderDataClient>();

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

// call seed db
PrepDb.PrepPopulation(app);

app.Run();
