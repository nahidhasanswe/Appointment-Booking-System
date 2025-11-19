using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Behaviors;

namespace AppointmentBooking.Application.Features.Appointments.Queries.Appointments;

public abstract class GetAppointmentsByPatientQuery : IQuery<Result<List<AppointmentDto>>>
{
    public Guid PatientId { get; set; }
}