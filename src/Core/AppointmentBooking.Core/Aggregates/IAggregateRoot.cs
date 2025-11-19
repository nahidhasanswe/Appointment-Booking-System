using AppointmentBooking.Core.Events;

namespace AppointmentBooking.Core.Aggregates;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}