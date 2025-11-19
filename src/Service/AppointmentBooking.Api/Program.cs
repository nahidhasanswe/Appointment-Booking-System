
using AppointmentBooking.Api.Configuration;
using AppointmentBooking.Application.Mapping;
using AppointmentBooking.Core.AutoMapper;
using AppointmentBooking.Core.Web.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddSwagger();

builder.Services.AddIoC(configuration);
builder.Services.AddObjectMapper(configuration["AutoMapper:LicenseKey"].ToString(), typeof(MappingProfile).Assembly);

var app = builder.Build();



app.UseHttpsRedirection();
app.UseErrorHandlingMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
