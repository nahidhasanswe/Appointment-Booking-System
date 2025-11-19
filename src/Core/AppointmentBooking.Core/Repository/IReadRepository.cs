using AppointmentBooking.Core.Collections;
using AppointmentBooking.Core.Entities;
using AppointmentBooking.Core.Specifications;

namespace AppointmentBooking.Core.Repository;

/// <summary>
/// Read-only repository interface for query operations only
/// </summary>
public interface IReadRepository<T, in TKey> where T: Entity<TKey>
{
    Task<IPagedList<T>> GetPaginationListAsync(Specification<T> specification, CancellationToken cancellationToken = default);
    ValueTask<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<int> CountAlAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
}

public interface IReadRepository<T> : IReadRepository<T, Guid> where T : Entity<Guid>
{
    
}