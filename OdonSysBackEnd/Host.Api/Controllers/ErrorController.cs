using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Host.Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private const string _problemPayloadType = "Error";
    public ErrorController(
        IWebHostEnvironment webHostEnvironment
    )
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("/error")]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        var (statusCode, title) = GetExceptionDetails(context);
        var message = string.IsNullOrEmpty(title) ? context.Error.Message : title;
        return Problem(
            detail: context.Error.StackTrace,
            title: message,
            statusCode: statusCode,
            type: _problemPayloadType);
    }

    private static (int StatusCode, string Title) GetExceptionDetails(IExceptionHandlerFeature exceptionDetails)
    {
        var statusCode = 500;
        var title = string.Empty;

        var exception = exceptionDetails?.Error;
        var innerException = exception?.InnerException;
        if (innerException != null)
        {
            if (innerException is SqlException)
            {
                var sqlException = innerException as SqlException;
                // https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-events-and-errors?view=sql-server-ver15
                // Cannot insert duplicate key row in object '%.*ls' with unique index '%.*ls'. The duplicate key value is %ls.
                // Cannot insert duplicate key row in object 'dbo.CustomFields' with unique index 'IX_CustomFields_Code'. The duplicate key value is (string).
                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    var duplicateKey = GetDuplicateKeyFromSqlErrorMessage(innerException.Message);
                    title = string.IsNullOrEmpty(duplicateKey) ? string.Empty : $"No es posible crear el recurso '{duplicateKey}' debido a que ya existe.";
                    statusCode = (int)HttpStatusCode.Conflict;
                }
                else if (sqlException.Number == 2628)
                {
                    //String or binary data would be truncated in table 'Table', column 'Column'. Truncated value: 'value'.\r\nThe statement has been terminated.
                    var message = sqlException.Message;
                    var column = message[message.LastIndexOf("column '")..message.IndexOf(". Truncated value: '")];
                    var value = message[message.LastIndexOf("value: '")..message.IndexOf(".\r\n")];
                    title = $"Error in {column} with {value}";
                    statusCode = (int)HttpStatusCode.Conflict;
                }
            }
        }
        else
        {
            if (exception is KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                title = $"Recurso con {exception.Message} no fue encontrado.";
            }
            if (exception is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                title = $"{exception.Message}.";
            }
            if (exception is UnauthorizedAccessException)
            {
                var unauthorizedException = exception as UnauthorizedAccessException;
                statusCode = (int)HttpStatusCode.Unauthorized;
                title = !string.IsNullOrEmpty(unauthorizedException.Message) ? unauthorizedException.Message : "Your credentials are invalid.";
            }
        }
        return (statusCode, title);
    }

    private static string GetDuplicateKeyFromSqlErrorMessage(string message)
    {
        var duplicateValue = string.Empty;
        var initialDuplicateIndex = message.IndexOf("(") + 1;
        var finalDuplicateIndex = message.IndexOf(")");

        return initialDuplicateIndex != -1 && finalDuplicateIndex != -1 ? message[initialDuplicateIndex..finalDuplicateIndex] : duplicateValue;
    }
}
