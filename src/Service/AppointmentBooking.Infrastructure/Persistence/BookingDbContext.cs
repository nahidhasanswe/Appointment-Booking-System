using System.Reflection;
using AppointmentBooking.Domain.Aggregates.AppointmentAggregate;
using AppointmentBooking.Domain.Aggregates.ClinicAggregate;
using AppointmentBooking.Domain.Aggregates.DoctorAggregate;
using AppointmentBooking.Domain.Aggregates.PatientAggregate;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Persistence;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<Clinic> Clinics => Set<Clinic>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}