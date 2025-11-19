using System.Collections.Generic;

namespace AppointmentBooking.Core.Collections
{
    public interface IPagedList<T>
    {
        IList<T> Items { get; set; }

        int TotalCount { get; set; }
    }
}
