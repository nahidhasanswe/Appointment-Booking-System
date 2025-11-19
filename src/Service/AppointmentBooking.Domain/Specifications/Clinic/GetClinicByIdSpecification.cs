using AppointmentBooking.Core.Specifications;

namespace AppointmentBooking.Domain.Specifications.Clinic;

public class GetClinicByIdSpecification(Guid id) : Specification<Aggregates.ClinicAggregate.Clinic>(c => c.Id == id);