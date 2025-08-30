using Microsoft.AspNetCore.Http;
using ShiftTrackingAPI.Models.DTO.Response;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace ShiftTrackingAPI.Helpers.Exceptions
{
    public class HandleException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleException> _logger;

        public HandleException(RequestDelegate next, ILogger<HandleException> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                _logger.LogWarning(ex, "Исключение: {Code}", ex.Type);

                context.Response.StatusCode = ex.Type switch
                {
                    ErrorType.NotFound => StatusCodes.Status400BadRequest,
                    ErrorType.DateIncorrect => StatusCodes.Status400BadRequest,
                    ErrorType.DataIncorrect => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status400BadRequest
                };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошло исключение не из списка исключений");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "ошибка сервера" }));
            }
        }
    }
}
