using CertificationManagementApp.BLRepository;
using CertificationManagementApp.DALRepository;
using CertificationManagementApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



builder.Services.AddDbContext<CertificationTrackerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CertificationTrackerDbContext"));
});


builder.Services.AddScoped<ICertificationBLRepository, CertificationBLRepository>();
builder.Services.AddScoped<ICertificationDALRepository, CertificationDALRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
