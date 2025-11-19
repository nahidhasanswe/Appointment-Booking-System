using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Behaviors;

namespace AppointmentBooking.Application.Features.Patient.Queries;

public sealed class GetPatientByIdQuery(Guid id) : IQuery<Result<PatientDto>>
{
    public Guid Id => id;
}