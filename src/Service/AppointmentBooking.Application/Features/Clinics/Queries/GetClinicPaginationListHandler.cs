using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Collections;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Domain.Aggregates.ClinicAggregate;
using AppointmentBooking.Domain.Specifications.Clinic;
using MediatR;

namespace AppointmentBooking.Application.Features.Clinics.Queries;

public class GetClinicPaginationListHandler(IClinicRepository clinicRepository, IObjectMapper mapper)
    : IRequestHandler<GetClinicPaginationListQuery, PaginationResult<ClinicDto>>
{
    public async Task<PaginationResult<ClinicDto>> Handle(GetClinicPaginationListQuery request, CancellationToken cancellationToken)
    {
        var searchSpecification = new GetAllClinicWithPaginationSpecification
        (
            request.Search,
            request.City,
            request.Country,
            request.Active,
            request.PageIndex,
            request.PageSize,
            request.Sort
        );
        
        var result = await clinicRepository.GetPaginationListAsync(searchSpecification, cancellationToken);
        
        return Result.SuccessForPagination(mapper.Map<IPagedList<ClinicDto>>(result));
    }
}