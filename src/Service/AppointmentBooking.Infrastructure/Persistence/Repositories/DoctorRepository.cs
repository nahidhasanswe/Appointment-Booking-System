using AppointmentBooking.Core.EF;
using AppointmentBooking.Core.EF.Repository;
using AppointmentBooking.Domain.Aggregates.DoctorAggregate;
using AppointmentBooking.Domain.Specifications.Doctor;

namespace AppointmentBooking.Infrastructure.Persistence.Repositories;

public class DoctorRepository(IDbContextProvider<BookingDbContext> dbContextProvider) : EfRepository<BookingDbContext, Doctor>(dbContextProvider), IDoctorRepository
{
    public Task<Doctor?> GetByIdWithSchedulesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return FirstOrDefaultAsync(new GetByIdWithSchedulesSpecification(id),  cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetByClinicIdAsync(Guid clinicId, CancellationToken cancellationToken = default)
    {
        return await ListAsync(new GetByClinicIdSpecification(clinicId), cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default)
    {
        return await ListAsync(new GetBySpecializationSpecification(specialization), cancellationToken);
    }

    public Task<bool> ExistsByLicenseNumberAsync(string licenseNumber, CancellationToken cancellationToken = default)
    {
        return AnyAsync(new GetByLicenseNumberSpecification(licenseNumber),  cancellationToken);
    }
}