using Access.Contract.Authentication;
using Access.Contract.Azure;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Utilities.Configurations;
using Utilities.Enums;

namespace Access.Data.Access
{
    internal class AzureAdB2CUserDataAccess : IAzureAdB2CUserDataAccess
    {
        private readonly GraphServiceClient _azureGraphServiceClient;
        private readonly AzureB2CSettings _azureB2CSettings;
        private readonly UserExtensionAccessModel _userExtensionAccessModel;

        public AzureAdB2CUserDataAccess(IOptions<AzureB2CSettings> azureB2COptions, IGraphService graphService)
        {
            _azureB2CSettings = azureB2COptions.Value;
            _userExtensionAccessModel = new UserExtensionAccessModel(_azureB2CSettings.ADApplicationB2CId);
            _azureGraphServiceClient = graphService.GetGraphServiceClient(_azureB2CSettings.TenantId, _azureB2CSettings.ClientId, _azureB2CSettings.ClientSecret);
        }

        public async Task<IEnumerable<UserGraphAccessModel>> GetUsersAsync()
        {
            var usersCollectionResponse = await _azureGraphServiceClient.Users.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Top = 999;
                    requestConfiguration.QueryParameters.Select = [
                        "id",
                        "displayName", // username
                        "mail",
                        "givenName", // name
                        "familyName", // surname
                        _userExtensionAccessModel.Document,
                        _userExtensionAccessModel.Phone,
                        "country",
                        _userExtensionAccessModel.SecondName,
                        _userExtensionAccessModel.SecondSurname,
                        "identities"
                    ];
                }
            );
            var users = usersCollectionResponse!.Value!.ToList();
            var userAccessModels = users.Select(user =>
            {
                var additionalData = user.AdditionalData;
                _ = additionalData.TryGetValue(_userExtensionAccessModel.Document, out var document);
                _ = additionalData.TryGetValue(_userExtensionAccessModel.Phone, out var phone);
                _ = additionalData.TryGetValue(_userExtensionAccessModel.SecondName, out var secondName);
                _ = additionalData.TryGetValue(_userExtensionAccessModel.SecondSurname, out var secondLastname);
                var roles = user.AppRoleAssignments.Select(x => x.PrincipalDisplayName);
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
                )
                {
                    Roles = roles
                };
            });
            return userAccessModels;
        }

        public async Task<UserGraphAccessModel> GetUserByIdAsync(string userId)
        {
            var user = await _azureGraphServiceClient.Users[userId].GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select =
                [
                    "id",
                    "displayName", // username
                    "email",
                    "givenName", // name
                    "surname", // surname
                    _userExtensionAccessModel.Document,
                    _userExtensionAccessModel.Phone,
                    "country",
                    _userExtensionAccessModel.SecondName,
                    _userExtensionAccessModel.SecondSurname,
                    "identities"
                ];
            });

            var additionalData = user.AdditionalData;
            _ = additionalData.TryGetValue(_userExtensionAccessModel.Document, out var document);
            _ = additionalData.TryGetValue(_userExtensionAccessModel.Phone, out var phone);
            _ = additionalData.TryGetValue(_userExtensionAccessModel.SecondName, out var secondName);
            _ = additionalData.TryGetValue(_userExtensionAccessModel.SecondSurname, out var secondLastname);
            var roles = user.AppRoleAssignments != null && user.AppRoleAssignments.Any() ? user.AppRoleAssignments?.Select(x => x.PrincipalDisplayName) : [];
            return new(
                user.Id!,
                user.DisplayName!,
                user.Identities.First(x => x.SignInType == _userExtensionAccessModel.EmailCode).IssuerAssignedId,
                user.GivenName!,
                user.Surname,
                document?.ToString(),
                phone?.ToString(),
                Enum.Parse<Country>(user.Country),
                secondName?.ToString(),
                secondLastname?.ToString()
            )
            {
                Roles = roles
            };
        }

        public async Task<string> UpdateUserAsync(string userId, string name, string surname)
        {
            var displayName = @$"{name[..1].ToUpper()}{surname[0].ToString().ToLower()}{surname[1..]}";
            var user = new Microsoft.Graph.Models.User
            {
                DisplayName = displayName
            };
            _ = await _azureGraphServiceClient.Users[userId].PatchAsync(user);
            return displayName;
        }
    }
}
