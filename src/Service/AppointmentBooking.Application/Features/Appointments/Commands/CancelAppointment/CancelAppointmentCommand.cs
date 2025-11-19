using AppointmentBooking.Core;
using MediatR;

namespace AppointmentBooking.Application.Features.Appointments.Commands.CancelAppointment;

public class CancelAppointmentCommand : IRequest<Result>
{
    public Guid AppointmentId { get; set; }
    public string Reason { get; set; } = string.Empty;
}