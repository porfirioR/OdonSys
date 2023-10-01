using Access.Contract.Azure;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Utilities.Configurations;
using Utilities.Enums;

namespace Access.Data.Access
{
    internal class AzureAdB2CUserDataAccess : IAzureAdB2CUserDataAccess
    {
        private readonly GraphServiceClient _azureGraphServiceClient;
        private readonly AzureB2CSettings _azureB2CSettings;
        private readonly UserExtensionAccessModel _userExtensionAccessModel;

        public AzureAdB2CUserDataAccess(IOptions<AzureB2CSettings> azureB2COptions)
        {
            _azureB2CSettings = azureB2COptions.Value;
            var clientSecretCredential = new ClientSecretCredential(
                _azureB2CSettings.TenantId,
                _azureB2CSettings.ClientId,
                _azureB2CSettings.ClientSecret
            );
            var scopes = new List<string>() {
                "https://graph.microsoft.com/.default"
            };
            _azureGraphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);
            _userExtensionAccessModel = new UserExtensionAccessModel(_azureB2CSettings.ADApplicationB2CId);
        }

        public async Task<IEnumerable<UserGraphAccessModel>> GetUsersAsync()
        {
            var usersCollectionResponse = await _azureGraphServiceClient.Users.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Top = 999;
                    requestConfiguration.QueryParameters.Select = new string[] {
                        "id",
                        "displayName", // username
                        "mail",
                        "givenName", // name
                        "familyName", // surname
                        _userExtensionAccessModel.Document,
                        _userExtensionAccessModel.Phone,
                        "country",
                        _userExtensionAccessModel.SecondName,
                        _userExtensionAccessModel.SecondLastname
                    };
                }
            );
            var users = usersCollectionResponse!.Value!.ToList();
            var userAccessModels = users.Select(user =>
            {
                var additionalData = user.AdditionalData;
                _ = additionalData.TryGetValue(_userExtensionAccessModel.Document, out var document);
                _ = additionalData.TryGetValue(_userExtensionAccessModel.Phone, out var phone);
                _ = additionalData.TryGetValue(_userExtensionAccessModel.SecondName, out var secondName);
                _ = additionalData.TryGetValue(_userExtensionAccessModel.SecondLastname, out var secondLastname);

                return new UserGraphAccessModel(
                    user.Id!,
                    user.DisplayName!,
                    user.Mail!,
                    user.GivenName!,
                    user.Surname,
                    document.ToString(),
                    phone.ToString(),
                    Enum.Parse<Country>(user.Country),
                    secondName.ToString(),
                    secondLastname.ToString()
                );
            });
            return userAccessModels;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var user = await _azureGraphServiceClient.Users[id].GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = new string[]
                {
                    "id",
                    "displayName", // username
                    "mail",
                    "givenName", // name
                    "familyName", // surname
                    _userExtensionAccessModel.Document,
                    _userExtensionAccessModel.Phone,
                    "country",
                    _userExtensionAccessModel.SecondName,
                    _userExtensionAccessModel.SecondLastname
                };
            });
            return user;
        }
    }
}
