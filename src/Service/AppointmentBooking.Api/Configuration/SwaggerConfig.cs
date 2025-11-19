using System.Reflection;
using AppointmentBooking.Api.Swagger;
using Microsoft.OpenApi.Models;

namespace AppointmentBooking.Api.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(cfg =>
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
            
            
            cfg.OperationFilter<CancellationTokenFilter>();
            
            cfg.OperationFilter<SwaggerTypeSetterFilter>();
            
            cfg.OperationFilter<SwaggerResponseTypesSetterFilter>();
            
        });

        return services;
    }
}