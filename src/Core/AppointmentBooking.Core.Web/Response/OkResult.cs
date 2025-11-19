using Microsoft.AspNetCore.Mvc;

namespace AppointmentBooking.Core.Web.Response
{
    public class OkResult<TResult> : OkObjectResult
        where TResult: class
    {
        public OkResult(TResult result)
        : base(new ApiResponse(result, "Success"))
        {
        }

        public OkResult(TResult result, string message)
        : base(new ApiResponse(result, message))
        {
        }
    }
}

