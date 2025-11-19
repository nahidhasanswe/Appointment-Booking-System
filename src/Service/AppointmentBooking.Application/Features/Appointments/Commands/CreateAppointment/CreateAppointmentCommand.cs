using AppointmentBooking.Core;
using MediatR;
using System;

namespace AppointmentBooking.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommand : IRequest<Result<Guid>>
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ClinicId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public DateTime AppointmentEndDateTime { get; set; }
    public string Status { get; set; } = string.Empty; // e.g., "Scheduled", "Completed", "Cancelled"
    public string? Notes { get; set; }
}