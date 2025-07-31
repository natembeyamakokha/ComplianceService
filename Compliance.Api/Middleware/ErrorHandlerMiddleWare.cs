using System.Net;
using Compliance.Shared;
using System.Text.Json;

namespace Compliance.Api
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        
        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SimSwapException error)
            {
                if (error.ValidationErrors != null) error.StatusMessage = error.FormattedError;

                _logger.LogError(error, error.Message);
                var response = context.Response;
                response.ContentType = "application/json";

                Result<string> serviceResponse = new()
                {
                    StatusCode = error.StatusCode,
                    StatusMessage = error.StatusMessage,
                };

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonSerializer.Serialize(serviceResponse);

                await response.WriteAsync(result);

            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                var response = context.Response;
                response.ContentType = "application/json";

                Result<string> serviceResponse = new()
                {
                    StatusCode = Shared.StatusCodes.INVALID_REQUEST,
                };

                if (_env.IsDevelopment() || _env.IsUat() || _env.IsLocal())
                {
                    serviceResponse.StatusMessage = error.Message;
                }
                else
                {
                    serviceResponse.StatusMessage = "an error occured";
                }


                // unhandled error
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(serviceResponse);

                await response.WriteAsync(result);
            }
        }
    }
}