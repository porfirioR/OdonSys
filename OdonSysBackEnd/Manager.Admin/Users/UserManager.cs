using Access.Contract;
using Access.Contract.Auth;
using Access.Contract.Users;
using AutoMapper;
using Resources.Contract.Auth;
using Resources.Contract.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Admin.Users
{
    internal class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IAuthDataAccess _authDataAccess;

        public UserManager(IMapper mapper, IUserDataAccess userDataAccess)
        {
            _mapper = mapper;
            _userDataAccess = userDataAccess;
        }

        public async Task<UserModel> Create(CreateUserRequest createUserRequest)
        {
            if (await CheckUserExistsAsync(createUserRequest))
            {
                new Exception("Ya existe un usuario con ese mismo documento.");
            }
            var dataAccess = _mapper.Map<UserDataAccessRequest>(createUserRequest);
            var accessModel = await _userDataAccess.CreateAsync(dataAccess);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<UserModel> Delete(string id)
        {
            var accessModel = await _userDataAccess.DeleteAsync(id);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<UserModel> Login(LoginRequest login)
        {
            var loginAccess = _mapper.Map<LoginDataAccess>(login);
            var accessModel = await _authDataAccess.Login(loginAccess);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var accessModel = await _userDataAccess.GetAll();
            var response = _mapper.Map<IEnumerable<UserModel>>(accessModel);
            return response;
        }

        public async Task<UserModel> GetById(string id)
        {
            var accessModel = await _userDataAccess.GetById(id);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<UserModel> Update(UpdateUserRequest updateUserRequest)
        {
            var dataAccess = _mapper.Map<UserDataAccessRequest>(updateUserRequest);
            var accessModel = await _userDataAccess.UpdateAsync(dataAccess);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        private async Task<bool> CheckUserExistsAsync(CreateUserRequest createUser)
        {
            var users = await _userDataAccess.GetAll();
            var result = users.Where(x => x.Document == createUser.Document);
            return users.Any();
        }
    }
}
