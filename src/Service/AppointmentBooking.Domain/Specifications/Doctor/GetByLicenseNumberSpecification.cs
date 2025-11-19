using AppointmentBooking.Core.Specifications;

namespace AppointmentBooking.Domain.Specifications.Doctor;

public class GetByLicenseNumberSpecification(string licenseNumber)
    : Specification<Aggregates.DoctorAggregate.Doctor>(c => c.LicenseNumber == licenseNumber);