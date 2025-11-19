using AppointmentBooking.Application.Configurations;
using AppointmentBooking.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Api.Configuration;

public static class DependencyConfig
{
    public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Repository and DB Context
        services.AddRepositories(x =>
        {
            x.UseInMemoryDatabase("AppointmentBookingSystem");
        });
        
        // Fluent Validation
        services.AddFluentValidationConfig();
        
        // MediatR Config
        services.AddMediatRConfiguration();

        return services;
    }
}