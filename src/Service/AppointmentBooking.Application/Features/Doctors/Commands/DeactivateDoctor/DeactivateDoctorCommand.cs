using AppointmentBooking.Core;
using MediatR;

namespace AppointmentBooking.Application.Features.Doctors.Commands.DeactivateDoctor;

public class DeactivateDoctorCommand : IRequest<Result>
{
    public Guid DoctorId { get; set; }
}
