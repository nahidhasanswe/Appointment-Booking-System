using AppointmentBooking.Core.EF;
using AppointmentBooking.Core.EF.Repository;
using AppointmentBooking.Domain.Aggregates.ClinicAggregate;
using AppointmentBooking.Domain.Specifications.Clinic;

namespace AppointmentBooking.Infrastructure.Persistence.Repositories;

public class ClinicRepository(IDbContextProvider<BookingDbContext> dbContextProvider) : EfRepository<BookingDbContext, Clinic>(dbContextProvider), IClinicRepository
{
    public async Task<IEnumerable<Clinic>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await ListAsync(new GetAllClinicSpecification(), cancellationToken);
    }

    public async Task<IEnumerable<Clinic>> GetByCity(string city, CancellationToken cancellationToken = default)
    {
        return await ListAsync(new GetAllClinicByCitySpecification(city), cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await AnyAsync(new GetClinicByIdSpecification(id), cancellationToken);
    }
}