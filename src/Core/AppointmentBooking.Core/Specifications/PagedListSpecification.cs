using System.Linq.Expressions;

namespace AppointmentBooking.Core.Specifications;

public abstract class PagedListSpecification<T> : Specification<T>
    where T : class
{
    protected PagedListSpecification(int pageIndex, int pageSize)
    {
        ApplyPaging(pageIndex * pageSize, pageSize);
    }
}