using AppointmentBooking.Application.Features.Patient.Commands.RegisterPatiend;
using AppointmentBooking.Core.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Application.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterPatientCommandValidator>();
        return services;
    }

    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));
        
        services.AddTransient(
            typeof(IPipelineBehavior<,>), 
            typeof(ValidationBehavior<,>));
        
        services.AddTransient(
            typeof(IPipelineBehavior<,>), 
            typeof(TransactionBehavior<,>));
        
        return services;
    }
}