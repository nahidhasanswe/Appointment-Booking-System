using AppointmentBooking.Core;
using MediatR;

namespace AppointmentBooking.Application.Features.Appointments.Commands.BookAppointment;

public class BookAppointmentCommand : IRequest<Result<Guid>>
{
    public Guid TimeSlotId { get; set; }
    public Guid PatientId { get; set; }
    public string? Notes { get; set; }
}