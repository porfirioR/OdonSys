using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Host.Api.Helpers
{
    [ExcludeFromCodeCoverage]
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly IAuthorizationMiddlewareResultHandler _defaultHandler;
        private const string ProblemPayloadType = "Error";

        public CustomAuthorizationMiddlewareResultHandler()
        {
            _defaultHandler = new AuthorizationMiddlewareResultHandler();
        }

        public async Task HandleAsync(RequestDelegate requestDelegate, HttpContext httpContext, AuthorizationPolicy authorizationPolicy, PolicyAuthorizationResult policyAuthorizationResult)
        {
            if (policyAuthorizationResult.Challenged)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = "application/problem+json";
                var serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                var details = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Title = "Credencial inválido",
                    Type = ProblemPayloadType
                };
                var jsonResponse = JsonConvert.SerializeObject(details, serializerSettings);
                await httpContext.Response.WriteAsync(jsonResponse);
                return;
            }
            if (policyAuthorizationResult.Forbidden && policyAuthorizationResult.AuthorizationFailure != null)
            {
                throw new UnauthorizedAccessException(policyAuthorizationResult.AuthorizationFailure.FailureReasons.First().Message);
            }

            await _defaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
        }
    }
}
