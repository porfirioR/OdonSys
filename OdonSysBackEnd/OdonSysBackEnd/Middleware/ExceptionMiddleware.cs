using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OdonSysBackEnd.Models.Error;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OdonSysBackEnd.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ApiException response;
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                if (ex is KeyNotFoundException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new ApiException(context.Response.StatusCode, ex.Message);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = _env.IsDevelopment()
                        ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                        : new ApiException(context.Response.StatusCode, "Error interno del servidor", ex.StackTrace?.ToString());
                }
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
