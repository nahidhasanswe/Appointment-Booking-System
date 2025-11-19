using AppointmentBooking.Core.Specifications;

namespace AppointmentBooking.Domain.Specifications.Doctor;

public sealed class GetByClinicIdSpecification : Specification<Aggregates.DoctorAggregate.Doctor>
{
    public GetByClinicIdSpecification(Guid clinicId)
        : base(x => x.ClinicId == clinicId && x.IsActive)
    {
        AddInclude(x => x.Clinic);
        ApplyOrderBy(x => x.FirstName);
    }
}