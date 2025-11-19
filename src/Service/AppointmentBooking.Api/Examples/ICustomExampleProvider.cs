using AppointmentBooking.Core.Web.Response;
using Swashbuckle.AspNetCore.Filters;

namespace AppointmentBooking.Api.Examples;

public interface ICustomExampleProvider<TModel> : IExamplesProvider<ApiResponse<TModel>>
    where TModel : class
{
    ApiResponse<TModel> GetResponse(TModel model);
}