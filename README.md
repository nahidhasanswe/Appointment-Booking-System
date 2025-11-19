# Appointment Booking System API

This repository contains the backend service for a modern appointment booking system, built on **.NET 9** and demonstrating a Clean Architecture approach.

## Key Technologies

- **Framework**: .NET 9, ASP.NET Core
- **Architecture**: Clean Architecture, Domain-Driven Design (DDD)
- **Core Patterns**: CQRS with MediatR, Repository & Specification Pattern
- **API Documentation**: Swashbuckle (Swagger/OpenAPI) with custom operation filters for examples.
- **Testing**: xUnit and Moq

## Project Layers

- **`Domain`**: Core entities and business logic.
- **`Application`**: DTOs, Commands, Queries, and application-layer logic.
- **`Api`**: ASP.NET Core host, controllers, and Swagger configuration.
- **`Core`**: Shared abstractions (`Result`, `ApiResponse`, specifications).
- **`tests`**: Unit tests for the API project.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A C# IDE (Visual Studio, JetBrains Rider, or VS Code)

### How to Run

1.  **Clone the repository.**
2.  **Navigate to the API project:**
    ```bash
    cd src/Service/AppointmentBooking.Api
    ```
3.  **Run the application:**
    ```bash
    dotnet run
    ```
4.  **Access the API documentation:**
    Open your browser to `https://localhost:5001/swagger` (port may vary).

## API Overview

The API provides RESTful endpoints for managing the core entities of the system:

- `/api/clinic`
- `/api/doctor`
- `/api/patient`
- `/api/appointment`

All list endpoints (`GET`) support pagination, searching, and sorting. `POST` and `PUT` endpoints include request and response examples in the Swagger documentation.
