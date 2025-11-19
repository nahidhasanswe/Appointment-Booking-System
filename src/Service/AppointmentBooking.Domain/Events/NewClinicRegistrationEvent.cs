using AppointmentBooking.Core.Events;

namespace AppointmentBooking.Domain.Events;

public class NewClinicRegistrationEvent(Guid clinicId) : DomainEvent()
{
    public Guid ClinicId { get; } = clinicId;
}