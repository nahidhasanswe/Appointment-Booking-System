using AppointmentBooking.Core.Repository;

namespace AppointmentBooking.Domain.Aggregates.DoctorAggregate;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetByIdWithSchedulesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetByClinicIdAsync(Guid clinicId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default);
    Task<bool> ExistsByLicenseNumberAsync(string licenseNumber, CancellationToken cancellationToken = default);
}