using Access.Contract.Authentication;
using Azure.Identity;
using Microsoft.Graph;

namespace Access.Data.Access;

internal sealed class GraphService : IGraphService
{
    public GraphServiceClient GetGraphServiceClient(string tenantId, string clientId, string clientSecret)
    {
        var scopes = new[] {
            "https://graph.microsoft.com/.default"
        };
        var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
        return graphClient;
    }
}
