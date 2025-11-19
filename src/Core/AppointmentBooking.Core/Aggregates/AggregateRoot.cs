using AppointmentBooking.Core.Entities;
using AppointmentBooking.Core.Events;

namespace AppointmentBooking.Core.Aggregates;

public abstract class AggregateRoot<TKey> : Entity<TKey>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TKey id) : base(id)
    {
    }

    protected IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public abstract class AggregateRoot : AggregateRoot<Guid>
{
    protected AggregateRoot() : base(Guid.NewGuid()) {}
    
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}