using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Domain.Aggregates.ClinicAggregate;
using MediatR;

namespace AppointmentBooking.Application.Features.Clinics.Queries;

public class GetClinicByIdQueryHandler(IClinicRepository clinicRepository, IObjectMapper mapper)
    : IRequestHandler<GetClinicByIdQuery, Result<ClinicDto>>
{
    public async Task<Result<ClinicDto>> Handle(GetClinicByIdQuery request, CancellationToken cancellationToken)
    {
        var clinic = await clinicRepository.GetByIdAsync(request.Id, cancellationToken);

        if (clinic is null)
            return Result.Failure<ClinicDto>("Clinic not found.");
        
        return Result.Success(mapper.Map<ClinicDto>(clinic));
    }
}