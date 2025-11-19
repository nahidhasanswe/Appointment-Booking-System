using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using MediatR;

namespace AppointmentBooking.Application.Features.Doctors.Queries.Schedule;

public abstract class GetScheduleByIdQuery : IRequest<Result<TimeSlotDto>>
{
    public Guid Id { get; set; }
}
