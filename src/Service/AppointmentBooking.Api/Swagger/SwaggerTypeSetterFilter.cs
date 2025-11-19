
using System.Net;
using System.Reflection;
using System.Text.Json;
using AppointmentBooking.Core.Web.Response;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AppointmentBooking.Api.Swagger;

public class SwaggerTypeSetterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var customAttribute = context.MethodInfo.GetCustomAttribute<ApiOperationMetadataAttribute>();
        if (customAttribute == null)
        {
            return;
        }

        var successStatusCode = ((int)HttpStatusCode.OK).ToString();

        // --- Handle Success Response (200 OK) ---
        var responseType = customAttribute.ResponseType;
        var genericResponseType = typeof(ApiResponse<>).MakeGenericType(responseType);

        // Ensure the 200 response is present
        if (!operation.Responses.ContainsKey(successStatusCode))
        {
            operation.Responses[successStatusCode] = new OpenApiResponse();
        }
        var okResponse = operation.Responses[successStatusCode];
        okResponse.Description = $"Successful response containing a {responseType.Name} entity.";

        // Ensure Content and media type are present
        if (okResponse.Content == null)
        {
            okResponse.Content = new Dictionary<string, OpenApiMediaType>();
        }
        if (!okResponse.Content.ContainsKey("application/json"))
        {
            okResponse.Content["application/json"] = new OpenApiMediaType();
        }
        var mediaType = okResponse.Content["application/json"];

        // Set the schema for the response
        mediaType.Schema = context.SchemaGenerator.GenerateSchema(genericResponseType, context.SchemaRepository);

        // Set the response example
        if (customAttribute?.ExampleType is not null)
        {
            try
            {
                var exampleProvider = Activator.CreateInstance(customAttribute.ExampleType);
                var getExamplesMethod = customAttribute.ExampleType.GetMethod("GetExamples");
                if (getExamplesMethod != null)
                {
                    var example = getExamplesMethod.Invoke(exampleProvider, null);
                    var json = JsonSerializer.Serialize(example, new JsonSerializerOptions { WriteIndented = true });
                    mediaType.Examples.Clear();
                    mediaType.Examples.Add("default", new OpenApiExample { Value = new Microsoft.OpenApi.Any.OpenApiString(json) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating Swagger response example for {customAttribute.ExampleType.Name}: {ex.Message}");
            }
        }

        // --- Handle Request Body (For ApiWriteResponseAttribute) ---
        if (customAttribute is ApiWriteResponseAttribute writeAttribute && writeAttribute.RequestType != null)
        {
            if (operation.RequestBody is null)
            {
                operation.RequestBody = new OpenApiRequestBody();
            }
            if (!operation.RequestBody.Content.ContainsKey("application/json"))
            {
                operation.RequestBody.Content["application/json"] = new OpenApiMediaType();
            }
            var requestMediaType = operation.RequestBody.Content["application/json"];

            // Set the schema for the request body
            requestMediaType.Schema = context.SchemaGenerator.GenerateSchema(writeAttribute.RequestType, context.SchemaRepository);

            // Set the request body example
            if (writeAttribute?.RequestExampleType is not null)
            {
                try
                {
                    var requestExampleProvider = Activator.CreateInstance(writeAttribute.RequestExampleType);
                    var getExamplesMethod = writeAttribute.RequestExampleType.GetMethod("GetExamples");
                    if (getExamplesMethod != null)
                    {
                        // Your examples return ApiResponse<T>, but the request body is just T.
                        // We need to unwrap the actual request object from the ApiResponse.
                        var exampleWrapper = getExamplesMethod.Invoke(requestExampleProvider, null);
                        var dataProperty = exampleWrapper?.GetType().GetProperty("Data");
                        var requestExample = dataProperty?.GetValue(exampleWrapper) ?? exampleWrapper;

                        var json = JsonSerializer.Serialize(requestExample, new JsonSerializerOptions { WriteIndented = true });
                        requestMediaType.Examples.Clear();
                        requestMediaType.Examples.Add("default", new OpenApiExample { Value = new Microsoft.OpenApi.Any.OpenApiString(json) });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating Swagger request example for {writeAttribute.RequestExampleType.Name}: {ex.Message}");
                }
            }
        }
    }
}
