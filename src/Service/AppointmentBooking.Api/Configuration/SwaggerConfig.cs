using System.Reflection;
using AppointmentBooking.Core.Swagger.Configuration;
using AppointmentBooking.Core.Web.Response;
using Microsoft.OpenApi.Models;

namespace AppointmentBooking.Api.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwagger(cfg =>
        {
            cfg.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Your API",
                Version = "v1"
            });

            // Include XML comments
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            cfg.IncludeXmlComments(xmlPath);
        }, typeof(ApiResponse<>));
        
        return services;
    }
}