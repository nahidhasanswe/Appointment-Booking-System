using AppointmentBooking.Api.Examples.Test;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Core.Swagger.Attributes;
using AppointmentBooking.Core.Web.Controller;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBooking.Api.Api;

[ApiController]
[Route("api/test")]
public class TestApiController(ILogger logger, IObjectMapper mapper) : ApiControllerBase(logger, mapper)
{
    [HttpGet]
    [ApiReadResponse(typeof(TestModel), typeof(TestModelExample))]
    public IActionResult GetExampleData()
    {
        return Ok();
    }
}