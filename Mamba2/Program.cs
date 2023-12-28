using FluentValidation.AspNetCore;
using Mamba.Business.Services.Implementations;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Repositories.using Mamba.Core.Repositories.?nterfaces;
using Mamba.Data.Repositories.Implementations;
using Mamba2.DAL;
using Mamba2.DTOs;
using Microsoft.EntityFrameworkCore;
using MyBiz.MappingProfiles;? nterfaces;
using Mamba.Data.Repositories.Implementations;
using Mamba2.DAL;
using Mamba2.DTOs;
using Microsoft.EntityFrameworkCore;
using MyBiz.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(typeof(EmployeeCreateDtoValidator).Assembly);

});
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default2"));
});
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped<IEmployeePositionRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IEmployeePositionRepository, EmployeePositionRepository>();

builder.Services.AddScoped<IPositionrepository, PositionRepository>();
builder.Services.AddScoped<IPositionService, PositionService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
