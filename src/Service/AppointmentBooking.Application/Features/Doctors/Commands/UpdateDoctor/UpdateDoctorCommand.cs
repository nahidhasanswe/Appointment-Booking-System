using AppointmentBooking.Application.Features.Doctors.Commands.CreateDoctor;

namespace AppointmentBooking.Application.Features.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorCommand : CreateDoctorCommand
{
    public Guid Id { get; set; }
}
