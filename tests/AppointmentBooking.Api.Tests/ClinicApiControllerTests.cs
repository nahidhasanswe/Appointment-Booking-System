using AppointmentBooking.Api.Api;
using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Application.Features.Clinics.Queries;
using AppointmentBooking.Core;
using AppointmentBooking.Core.Mapping;
using AppointmentBooking.Core.Web.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AppointmentBooking.Api.Tests;

public class ClinicApiControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IObjectMapper> _mapperMock;
    private readonly Mock<ILogger<ClinicApiController>> _loggerMock;
    private readonly ClinicApiController _controller;

    public ClinicApiControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IObjectMapper>();
        _loggerMock = new Mock<ILogger<ClinicApiController>>();

        _controller = new ClinicApiController(_loggerMock.Object, _mapperMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsOkObjectResult_WithClinicDto()
    {
        // Arrange
        var clinicId = Guid.NewGuid();
        var clinicDto = new ClinicDto { Id = clinicId, Name = "Test Clinic" };
        var successResult = Result.Success(clinicDto);

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetClinicByIdQuery>(q => q.Id == clinicId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(successResult);

        // Act
        var result = await _controller.GetByIdAsync(clinicId);

        // Assert
        var okResult = Assert.IsType<OkResult<ClinicDto>>(result);
        var apiResponse = Assert.IsType<Core.Web.Response.ApiResponse>(okResult.Value);
        Assert.True(apiResponse.Status);
        Assert.Equal(clinicId, ((ClinicDto)apiResponse.Data).Id);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var clinicId = Guid.NewGuid();
        var failureResult = Result.Failure<ClinicDto>("Clinic not found.");

        _mediatorMock
            .Setup(m => m.Send<Result<ClinicDto>>(It.Is<GetClinicByIdQuery>(q => q.Id == clinicId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(failureResult);

        // Act
        var result = await _controller.GetByIdAsync(clinicId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var apiResponse = Assert.IsType<Core.Web.Response.ApiResponse>(notFoundResult.Value);
        Assert.False(apiResponse.Status);
        Assert.Contains("not found", apiResponse.Message, StringComparison.OrdinalIgnoreCase);
    }
}
