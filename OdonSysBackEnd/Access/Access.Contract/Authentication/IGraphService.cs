using Microsoft.Graph;

namespace Access.Contract.Authentication;

public interface IGraphService
{
    GraphServiceClient GetGraphServiceClient(string tenantId, string clientId, string clientSecret);
}
