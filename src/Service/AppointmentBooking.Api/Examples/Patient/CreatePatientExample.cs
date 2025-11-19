using AppointmentBooking.Core.Web.Response;
using AppointmentBooking.Application.Features.Patient.Commands.RegisterPatiend;

namespace AppointmentBooking.Api.Examples.Patient;

public class CreatePatientExample : CustomExampleProvider<RegisterPatientCommand>
{
    public override ApiResponse<RegisterPatientCommand> GetExamples()
    {
        var model = new RegisterPatientCommand
        {
            FirstName = "Alice",
            LastName = "Smith",
            DateOfBirth = new DateTime(1995, 8, 20),
            Gender = "Female",
            Email = "alice.smith@example.com",
            Phone = "555-111-3333",
            Street = "123 Maple Ave",
            City = "Springfield",
            Country = "USA",
            EmergencyContact = "Bob Smith (Brother) - 555-222-4444"
        };

        return GetResponse(model);
    }
}