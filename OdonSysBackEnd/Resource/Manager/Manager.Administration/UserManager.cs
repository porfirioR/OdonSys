using Access.Contract.Authentication;
using Access.Contract.Azure;
using Access.Contract.Users;
using Contract.Administration.Authentication;
using Contract.Administration.Users;
using Microsoft.Graph.Models;
using System.Security.Claims;
using System.Text;
using Utilities;

namespace Manager.Administration
{
    internal sealed class UserManager : IUserManager
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IAuthenticationAccess _authenticationDataAccess;
        private readonly IUserManagerBuilder _userManagerBuilder;
        private readonly IAzureAdB2CUserDataAccess _azureAdB2CUserDataAccess;

        public UserManager(
            IUserDataAccess userDataAccess,
            IAuthenticationAccess authenticationDataAccess,
            IUserManagerBuilder userManagerBuilder,
            IAzureAdB2CUserDataAccess azureAdB2CUserDataAccess
        )
        {
            _userDataAccess = userDataAccess;
            _authenticationDataAccess = authenticationDataAccess;
            _userManagerBuilder = userManagerBuilder;
            _azureAdB2CUserDataAccess = azureAdB2CUserDataAccess;
        }

        public async Task<AuthenticationModel> RegisterUserAsync(RegisterUserRequest createUserRequest)
        {
            if (await CheckUserExistsAsync(createUserRequest))
            {
                throw new AggregateException("Ya existe un usuario con ese mismo documento o correo.");
            }
            var accessRequest = _userManagerBuilder.MapRegisterUserRequestToUserDataAccessRequest(createUserRequest);
            var accessModel = await _authenticationDataAccess.RegisterUserAsync(accessRequest);
            var response = _userManagerBuilder.MapAuthenticationAccessModelToAuthenticationModel(accessModel);
            return response;
        }

        public async Task<AuthenticationModel> LoginAsync(string authorization)
        {
            var encodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorization["Basic ".Length..]));
            var credentials = encodedCredentials.Split(":");
            var loginAccess = new LoginDataAccess(credentials.First(), credentials.Last());
            var accessModel = await _authenticationDataAccess.LoginAsync(loginAccess);
            var model = _userManagerBuilder.MapAuthenticationAccessModelToAuthenticationModel(accessModel);
            return model;
        }

        public async Task<UserModel> ApproveNewUserAsync(string id)
        {
            var accessModel = await _userDataAccess.ApproveNewUserAsync(id);
            var model = _userManagerBuilder.MapUserDataAccessModelToUserModel(accessModel);
            return model;
        }

        public async Task<IEnumerable<DoctorModel>> GetAllAsync()
        {
            var accessModel = await _userDataAccess.GetAllAsync();
            var response = accessModel.Select(_userManagerBuilder.MapDoctorDataAccessModelToDoctorModel);
            return response;
        }

        public async Task<IEnumerable<string>> SetUserRolesAsync(UserRolesRequest request)
        {
            var accessRequest = new UserRolesAccessRequest(request.UserId, request.Roles);
            var userRoles = await _userDataAccess.SetUserRolesAsync(accessRequest);
            return userRoles;
        }

        public bool RemoveAllClaims(ClaimsPrincipal claimsPrincipal)
        {
            var success = _authenticationDataAccess.RemoveAllClaims(claimsPrincipal);
            return success;
        }

        public async Task<DoctorModel> GetByIdAsync(string id)
        {
            var accessModel = await _userDataAccess.GetByIdAsync(id);
            var model = _userManagerBuilder.MapDoctorDataAccessModelToDoctorModel(accessModel);
            return model;
        }

        public async Task<UserClientModel> GetUserClientAsync(string userId, string clientId)
        {
            var accessModel = await _userDataAccess.GetUserClientAsync(new UserClientAccessRequest(clientId, userId));
            var model = new UserClientModel(accessModel.Id, accessModel.UserId, accessModel.ClientId);
            return model;
        }

        public async Task<DoctorModel> UpdateAsync(UpdateDoctorRequest updateUserRequest)
        {
            var dataAccess = _userManagerBuilder.MapUpdateDoctorRequestToUserDataAccessRequest(updateUserRequest);
            var accessModel = await _userDataAccess.UpdateAsync(dataAccess);
            var model = _userManagerBuilder.MapDoctorDataAccessModelToDoctorModel(accessModel);
            return model;
        }

        public async Task<IEnumerable<string>> CheckUsersExistsAsync(IEnumerable<string> users)
        {
            var usersAccess = (await _userDataAccess.GetAllUserAsync())
                                    .Where(x => x.Active)
                                    .Select(x => x.UserName);

            var invalidOrInactiveList = users.Where(x => !usersAccess.Contains(x));
            return invalidOrInactiveList;
        }

        private async Task<bool> CheckUserExistsAsync(RegisterUserRequest createUser)
        {
            var users = await _userDataAccess.GetAllAsync();
            var sameUserData = users.Where(x => x.Document == createUser.Document || x.Email == createUser.Email);
            return sameUserData.Any();
        }

        public async Task<IEnumerable<DoctorModel>> GetAllUsersByAzureAsync()
        {
            var accessModelList = await _azureAdB2CUserDataAccess.GetUsersAsync();
            var modelList = accessModelList.Select(_userManagerBuilder.MapUserGraphAccessModelToDoctorModel);
            return modelList;
        }

        public async Task<UserModel> GetUserFromGraphApiByIdAsync(string id)
        {
            var userDataAccess = await _userDataAccess.GetByIdAsync(id);
            var adB2CUserAccessModel = await _azureAdB2CUserDataAccess.GetUserByIdAsync(id);
            adB2CUserAccessModel.Approved = userDataAccess.Approved;
            adB2CUserAccessModel.Active = userDataAccess.Active;
            adB2CUserAccessModel.Roles = userDataAccess.Roles;
            var model = _userManagerBuilder.MapUserGraphAccessModelToUserModel(adB2CUserAccessModel);
            return model;
        }

        public async Task<UserModel> RegisterUserAsync(string externalUserId)
        {
            var userGraphAccessModel = await _azureAdB2CUserDataAccess.GetUserByIdAsync(externalUserId);
            var userName = await _azureAdB2CUserDataAccess.UpdateUserAsync(externalUserId, userGraphAccessModel.Name, userGraphAccessModel.Surname);
            var accessRequest = _userManagerBuilder.MapUserGraphAccessModelToRegisterUserRequest(userGraphAccessModel);
            var acceassModel = await _authenticationDataAccess.RegisterAzureAdB2CUserAsync(accessRequest);
            var model = _userManagerBuilder.MapUserDataAccessModelToUserModel(acceassModel);
            return model;
        }
    }
}
