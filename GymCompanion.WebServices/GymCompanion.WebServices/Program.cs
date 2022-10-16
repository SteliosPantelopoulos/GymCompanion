using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GymCompanion.WebServices.Controllers;
using GymCompanion.WebServices.DAL;
//using GymCompanion.WebServices.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<GymCompanionContext>(option => option.UseSqlServer(configuration.GetConnectionString("GymCompanionDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#if !DEBUG
    app.UseHttpsRedirection();
#endif

app.UseAuthorization();

app.MapControllers();

app.Run();
