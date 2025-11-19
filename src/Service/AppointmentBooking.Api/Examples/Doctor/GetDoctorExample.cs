using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Core.Web.Response;
using System;

namespace AppointmentBooking.Api.Examples.Doctor;

public class GetDoctorExample : CustomExampleProvider<DoctorDto>
{
    public override ApiResponse<DoctorDto> GetExamples()
    {
        var model = new DoctorDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            FullName = "John Doe",
            Specialization = "Cardiology",
            LicenseNumber = "MD12345",
            Phone = "555-123-4567",
            Email = "john.doe@example.com",
            ClinicId = Guid.NewGuid(),
            ClinicName = "Central Clinic",
            IsActive = true
        };

        return GetResponse(model);
    }
}