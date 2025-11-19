using MediatR;

namespace AppointmentBooking.Core.Behaviors;

public interface IQuery<out TResponse> : IRequest<TResponse> { }

public interface IPaginationQuery<out TResponse> : IQuery<TResponse>
{
    int PageIndex { get; set; }
    int PageSize { get; set; }
}