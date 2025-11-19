using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Behaviors;

namespace AppointmentBooking.Application.Features.Doctors.Queries.Doctor;

public sealed class GetDoctorByIdQuery(Guid id): IQuery<Result<DoctorDto>>
{
    public Guid Id => id;
}