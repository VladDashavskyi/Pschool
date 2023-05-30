using PSchool.API.DAL.Contexts;
using PSchool.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Pschool.API.Interfaces;
using Pschool.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy"
                    , builder =>
                    {
                        builder.AllowAnyHeader()
                                                            .AllowAnyMethod()
                                                            .AllowAnyOrigin();
                    });
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<TenantContext>(options =>
                                                           options.UseSqlServer(builder.Configuration
                                                                                   .GetConnectionString("WebApiDatabase")), ServiceLifetime.Transient);

builder.Services.AddScoped<ITenantContext, TenantContext>();

builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
});

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
