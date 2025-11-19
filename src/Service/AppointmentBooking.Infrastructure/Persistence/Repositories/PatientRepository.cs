using AppointmentBooking.Core.EF;
using AppointmentBooking.Core.EF.Repository;
using AppointmentBooking.Domain.Aggregates.PatientAggregate;
using AppointmentBooking.Domain.Specifications.Patient;

namespace AppointmentBooking.Infrastructure.Persistence.Repositories;

public class PatientRepository(IDbContextProvider<BookingDbContext> dbContextProvider) : EfRepository<BookingDbContext, Patient>(dbContextProvider), IPatientRepository
{
    public Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return FirstOrDefaultAsync(new GetByEmailSpecification(email), cancellationToken);
    }

    public Task<Patient?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default)
    {
        return FirstOrDefaultAsync(new GetByPhoneSpecification(phone), cancellationToken);
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return AnyAsync(new GetByEmailSpecification(email), cancellationToken);
    }

    public Task<bool> ExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default)
    {
        return AnyAsync(new GetByPhoneSpecification(phone), cancellationToken);
    }
}