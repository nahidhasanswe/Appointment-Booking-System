using AppointmentBooking.Core;
using MediatR;

namespace AppointmentBooking.Application.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}