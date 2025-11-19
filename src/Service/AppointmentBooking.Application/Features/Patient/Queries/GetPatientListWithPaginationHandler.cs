using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Collections;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Domain.Aggregates.PatientAggregate;
using AppointmentBooking.Domain.Specifications.Patient;
using MediatR;

namespace AppointmentBooking.Application.Features.Patient.Queries;

public class GetPatientListWithPaginationHandler(IPatientRepository patientRepository, IObjectMapper mapper)
    : IRequestHandler<GetPatientListWithPaginationQuery, PaginationResult<PatientDto>>
{
    public async Task<PaginationResult<PatientDto>> Handle(GetPatientListWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var searchSpecification = new GetPatientListWithPaginationSpecification
            (
                request.Search,
                request.Gender,
                request.City,
                request.Country,
                request.FromDateOfBirth,
                request.ToDateOfBirth,
                request.PageIndex,
                request.PageSize,
                request.Sort
            );
        
        var result = await patientRepository.GetPaginationListAsync(searchSpecification, cancellationToken);
        
        return Result.SuccessForPagination<PatientDto>(mapper.Map<IPagedList<PatientDto>>(result));
    }
}