﻿using Access.Contract.Authentication;
using Access.Contract.Users;
using AutoMapper;
using Contract.Administration.Authentication;
using Contract.Administration.Users;
using System.Security.Claims;
using System.Text;

namespace Manager.Administration
{
    internal sealed class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IAuthenticationAccess _authDataAccess;

        public UserManager(IMapper mapper, IUserDataAccess userDataAccess, IAuthenticationAccess authDataAccess)
        {
            _mapper = mapper;
            _userDataAccess = userDataAccess;
            _authDataAccess = authDataAccess;
        }

        public async Task<AuthenticationModel> RegisterUserAsync(RegisterUserRequest createUserRequest)
        {
            if (await CheckUserExistsAsync(createUserRequest))
            {
                throw new AggregateException("Ya existe un usuario con ese mismo documento o correo.");
            }
            var dataAccess = _mapper.Map<UserDataAccessRequest>(createUserRequest);
            var accessModel = await _authDataAccess.RegisterUserAsync(dataAccess);
            var response = _mapper.Map<AuthenticationModel>(accessModel);
            return response;
        }

        public async Task<AuthenticationModel> LoginAsync(string authorization)
        {
            var encodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorization["Basic ".Length..]));
            var credentials = encodedCredentials.Split(":");
            var loginAccess = new LoginDataAccess(credentials.First(), credentials.Last());
            var accessModel = await _authDataAccess.LoginAsync(loginAccess);
            var model = _mapper.Map<AuthenticationModel>(accessModel);
            return model;
        }

        public async Task<UserModel> ApproveNewUserAsync(string id)
        {
            var accessModel = await _userDataAccess.ApproveNewUserAsync(id);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<IEnumerable<DoctorModel>> GetAllAsync()
        {
            var accessModel = await _userDataAccess.GetAllAsync();
            var response = _mapper.Map<IEnumerable<DoctorModel>>(accessModel);
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
            var success = _authDataAccess.RemoveAllClaims(claimsPrincipal);
            return success;
        }

        public async Task<DoctorModel> GetByIdAsync(string id)
        {
            var accessModel = await _userDataAccess.GetByIdAsync(id);
            var model = _mapper.Map<DoctorModel>(accessModel);
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
            var dataAccess = _mapper.Map<UserDataAccessRequest>(updateUserRequest);
            var accessModel = await _userDataAccess.UpdateAsync(dataAccess);
            var model = _mapper.Map<DoctorModel>(accessModel);
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
    }
}