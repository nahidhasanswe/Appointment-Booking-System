using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Behaviors;

namespace AppointmentBooking.Application.Features.Patient.Queries;

public sealed class GetPatientListWithPaginationQuery : IPaginationQuery<PaginationResult<PatientDto>>
{
    public string Search { get; set; } = string.Empty;
    public string? Gender { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public DateTime? FromDateOfBirth { get; set; } = null;
    public DateTime? ToDateOfBirth { get; set; } = null;

    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 50;

    public string? Sort { get; set; } = null;
}