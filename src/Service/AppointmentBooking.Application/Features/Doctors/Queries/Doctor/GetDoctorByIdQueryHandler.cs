using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Domain.Aggregates.DoctorAggregate;
using AppointmentBooking.Domain.Specifications.Doctor;
using MediatR;

namespace AppointmentBooking.Application.Features.Doctors.Queries.Doctor;

public class GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository, IObjectMapper mapper) : IRequestHandler<GetDoctorByIdQuery, Result<DoctorDto>>
{
    public async Task<Result<DoctorDto>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.FirstOrDefaultAsync(new GetDoctorByIdSpecification(request.Id), cancellationToken);

        if (doctor is null)
            return Result.Failure<DoctorDto>("Doctor does not exist");
        
        return Result.Success(mapper.Map<DoctorDto>(doctor));
    }
}