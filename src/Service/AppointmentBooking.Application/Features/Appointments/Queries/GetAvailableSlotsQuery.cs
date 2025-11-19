using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Behaviors;

namespace AppointmentBooking.Application.Features.Appointments.Queries;

public abstract class GetAvailableSlotsQuery : IQuery<Result<List<TimeSlotDto>>>
{
    public Guid DoctorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}