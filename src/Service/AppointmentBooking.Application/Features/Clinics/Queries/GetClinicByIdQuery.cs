using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Behaviors;

namespace AppointmentBooking.Application.Features.Clinics.Queries;

public sealed class GetClinicByIdQuery(Guid id) : IQuery<Result<ClinicDto>>
{
    public Guid Id { get; private set; } = id;
}