using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using MediatR;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Domain.Specifications.Appointment;
using AppointmentBooking.Core.Collections;
using AppointmentBooking.Domain.Aggregates.AppointmentAggregate;

namespace AppointmentBooking.Application.Features.Appointments.Queries.Appointments;

public class GetAppointmentPaginationListQueryHandler(
    IAppointmentRepository appointmentRepository,
    IObjectMapper mapper)
    : IRequestHandler<GetAppointmentPaginationListQuery, PaginationResult<AppointmentDto>>
{
    public async Task<PaginationResult<AppointmentDto>> Handle(GetAppointmentPaginationListQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetAppointmentPaginationListSpecification(
            request.PatientId,
            request.DoctorId,
            request.ClinicId,
            request.StartDate,
            request.EndDate,
            request.Status,
            request.PageIndex,
            request.PageSize,
            request.Sort
        );

        var response = await appointmentRepository.GetPaginationListAsync(spec, cancellationToken);
        return Result.SuccessForPagination<AppointmentDto>(mapper.Map<IPagedList<AppointmentDto>>(response));
    }
}
