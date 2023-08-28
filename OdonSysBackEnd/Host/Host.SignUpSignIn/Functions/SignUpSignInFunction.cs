using Host.SignUpSignIn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Host.SignUpSignIn.Functions;

public static class SignUpSignInFunction
{
    [FunctionName("SignUpFunction")]
    public static async Task<IActionResult> SignUpFunction(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
        ILogger log)
    {
        log.LogInformation("HTTP Sign Up Function.");
        using var streamReader = new StreamReader(request.Body);
        var requestBody = await streamReader.ReadToEndAsync();
        var azureADApiRequest = JsonConvert.DeserializeObject<AzureActiveB2CApiRequest>(requestBody)!;
        var responseMessage = "all success.";
        var returnedClaims = new List<AttributePropertyModel>
        {
            new AttributePropertyModel("Step", azureADApiRequest.Step),
            new AttributePropertyModel("client_id", azureADApiRequest.Client_Id),
            new AttributePropertyModel("objectId", azureADApiRequest.ObjectId),
            new AttributePropertyModel("ui_locales", azureADApiRequest.Ui_locales),
            new AttributePropertyModel("Email", azureADApiRequest.Email),
            new AttributePropertyModel("Surname", azureADApiRequest.Surname),
            new AttributePropertyModel("GivenName", azureADApiRequest.GivenName),
            new AttributePropertyModel("DisplayName", $"{azureADApiRequest.GivenName.First()}{azureADApiRequest.Surname}"),
            new AttributePropertyModel("Roles", "superAdmin")
        };

        return GetB2cApiConnectorResponse("Continue", responseMessage, 200, returnedClaims, log);
    }

    [FunctionName("SignInFunction")]
    public static async Task<IActionResult> SignInFunction(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
        ILogger log)
    {
        log.LogInformation("HTTP Sign In Function.");
        using var streamReader = new StreamReader(request.Body);
        var requestBody = await streamReader.ReadToEndAsync();
        var azureADApiRequest = JsonConvert.DeserializeObject<AzureActiveB2CApiRequest>(requestBody)!;
        var responseMessage = "all success.";
        var returnedClaims = new List<AttributePropertyModel>
        {
            new AttributePropertyModel("Step", azureADApiRequest.Step!),
            new AttributePropertyModel("client_id", azureADApiRequest.Client_Id),
            new AttributePropertyModel("ui_locales", azureADApiRequest.Ui_locales),
            new AttributePropertyModel("Email", azureADApiRequest.Email),
            new AttributePropertyModel("Surname", azureADApiRequest.Surname),
            new AttributePropertyModel("GivenName", azureADApiRequest.GivenName),
            new AttributePropertyModel("DisplayName", azureADApiRequest.DisplayName),
            new AttributePropertyModel("Roles", azureADApiRequest.Roles)
        };

        return GetB2cApiConnectorResponse("Continue", responseMessage, 200, returnedClaims, log);
    }

    private static IActionResult GetB2cApiConnectorResponse(string action, string userMessage, int statusCode, IEnumerable<AttributePropertyModel> extensionProperty, ILogger log)
    {
        var responseProperties = new Dictionary<string, object>
        {
            { "version", "1.0.0" },
            { "action", action },
            { "userMessage", userMessage },
            { "status", statusCode }
        };
        foreach (var item in extensionProperty)
        {
            responseProperties.Add(item.Attribute, item.Value);
            log.LogInformation($"{item.Attribute}: {item.Value}");
        }
        if (statusCode != 200)
        {
            responseProperties["status"] = statusCode.ToString(CultureInfo.InvariantCulture);
        }
        log.LogInformation($"End Function.");
        return new JsonResult(responseProperties)
        {
            StatusCode = statusCode,
            ContentType = "application/json"
        };
    }
}
