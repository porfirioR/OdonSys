using Access.Contract.Auth;
using Access.Contract.Users;
using AutoMapper;
using Contract.Admin.Auth;
using Contract.Admin.User;
using Contract.Admin.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Admin
{
    internal class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IAuthDataAccess _authDataAccess;

        public UserManager(IMapper mapper, IUserDataAccess userDataAccess, IAuthDataAccess authDataAccess)
        {
            _mapper = mapper;
            _userDataAccess = userDataAccess;
            _authDataAccess = authDataAccess;
        }

        public async Task<AuthModel> RegisterUserAsync(RegisterUserRequest createUserRequest)
        {
            if (await CheckUserExistsAsync(createUserRequest))
            {
                throw new AggregateException("Ya existe un usuario con ese mismo documento o correo.");
            }
            var dataAccess = _mapper.Map<UserDataAccessRequest>(createUserRequest);
            var accessModel = await _authDataAccess.RegisterUserAsync(dataAccess);
            var response = _mapper.Map<AuthModel>(accessModel);
            return response;
        }

        public async Task<AuthModel> LoginAsync(LoginRequest login)
        {
            var loginAccess = _mapper.Map<LoginDataAccess>(login);
            var accessModel = await _authDataAccess.LoginAsync(loginAccess);
            var model = _mapper.Map<AuthModel>(accessModel);
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

        public async Task<DoctorModel> GetByIdAsync(string id)
        {
            var accessModel = await _userDataAccess.GetByIdAsync(id);
            var model = _mapper.Map<DoctorModel>(accessModel);
            return model;
        }

        public async Task<DoctorModel> UpdateAsync(UpdateDoctorRequest updateUserRequest)
        {
            var dataAccess = _mapper.Map<UserDataAccessRequest>(updateUserRequest);
            var accessModel = await _userDataAccess.UpdateAsync(dataAccess);
            var model = _mapper.Map<DoctorModel>(accessModel);
            return model;
        }

        private async Task<bool> CheckUserExistsAsync(RegisterUserRequest createUser)
        {
            var users = await _userDataAccess.GetAllAsync();
            var sameUserData = users.Where(x => x.Document == createUser.Document || x.Email == createUser.Email);
            return sameUserData.Any();
        }
    }
}
