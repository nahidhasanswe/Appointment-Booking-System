using AppointmentBooking.Core.EF;
using AppointmentBooking.Core.EF.Uow;
using AppointmentBooking.Core.Uow;
using AppointmentBooking.Infrastructure.Persistence;
using AppointmentBooking.Infrastructure.Persistence.Repositories;
using AppointmentBooking.Domain.Aggregates.AppointmentAggregate;
using AppointmentBooking.Domain.Aggregates.ClinicAggregate;
using AppointmentBooking.Domain.Aggregates.DoctorAggregate;
using AppointmentBooking.Domain.Aggregates.PatientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories
        (
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> dbOptionsBuilder
        )
    {
        services.AddDbContextPool<BookingDbContext>(dbOptionsBuilder);
        services.AddScoped<IDbContextProvider<BookingDbContext>, DefaultDbContextProvider<BookingDbContext>>();
        services.AddScoped<IUnitOfWorkManager, EfCoreUnitOfWorkManager<BookingDbContext>>();


        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IClinicRepository, ClinicRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        
        return services;
    }
}