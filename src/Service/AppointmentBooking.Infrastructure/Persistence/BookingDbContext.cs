using System.Reflection;
using AppointmentBooking.Core.Aggregates;
using AppointmentBooking.Core.Entities;
using AppointmentBooking.Domain.Aggregates.AppointmentAggregate;
using AppointmentBooking.Domain.Aggregates.ClinicAggregate;
using AppointmentBooking.Domain.Aggregates.DoctorAggregate;
using AppointmentBooking.Domain.Aggregates.PatientAggregate;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Persistence;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options)
    {
    }

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
        // Update timestamps
        var entries = ChangeTracker.Entries<Entity>()
            .Where(e => e.State == EntityState.Modified);

        // foreach (var entry in entries)
        // {
        //     entry.Entity.UpdatedAt = DateTime.UtcNow;
        // }

        // Dispatch domain events
        // var domainEvents = ChangeTracker.Entries<AggregateRoot>()
        //     .Select(e => e.Entity)
        //     .ToList() // Materialize first
        //     .Where(e => e.DomainEvents.Any())
        //     .SelectMany(e => e.DomainEvents)
        //     .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        // Clear domain events after saving
        foreach (var entity in ChangeTracker.Entries<AggregateRoot>().Select(e => e.Entity))
        {
            entity.ClearDomainEvents();
        }

        return result;
    }
}