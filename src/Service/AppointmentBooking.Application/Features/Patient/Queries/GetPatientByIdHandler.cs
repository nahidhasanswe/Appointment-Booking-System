using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Domain.Aggregates.PatientAggregate;
using MediatR;

namespace AppointmentBooking.Application.Features.Patient.Queries;


public class GetPatientByIdHandler(IPatientRepository patientRepository, IObjectMapper mapper)
    : IRequestHandler<GetPatientByIdQuery, Result<PatientDto>>
{
    public async Task<Result<PatientDto>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient is null)
            return Result.Failure<PatientDto>("Patient not found.");
        
        return Result.Success(mapper.Map<PatientDto>(patient));
    }
}