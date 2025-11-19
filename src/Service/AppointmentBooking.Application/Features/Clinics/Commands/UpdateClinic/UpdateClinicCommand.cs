using AppointmentBooking.Application.Features.Clinics.Commands.CreateClinic;

namespace AppointmentBooking.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommand : CreateClinicCommand
{
    public Guid Id { get; set; }
}