using Access.Contract.Auth;
using Access.Contract.Users;
using AutoMapper;
using Contract.Authentication.User;
using Contract.Workspace.User;

namespace Manager.Authentication.Users
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

        public async Task<UserModel> RegisterUserAsync(RegisterUserRequest createUserRequest)
        {
            if (await CheckUserExistsAsync(createUserRequest))
            {
                throw new AggregateException("Ya existe un usuario con ese mismo documento o correo.");
            }
            var dataAccess = _mapper.Map<UserDataAccessRequest>(createUserRequest);
            var accessModel = await _authDataAccess.RegisterUserAsync(dataAccess);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<UserModel> DeactivateAsync(string id)
        {
            var accessModel = await _userDataAccess.DeleteAsync(id);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<UserModel> LoginAsync(LoginRequest login)
        {
            var loginAccess = _mapper.Map<LoginDataAccess>(login);
            var accessModel = await _authDataAccess.Login(loginAccess);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var accessModel = await _userDataAccess.GetAll();
            var response = _mapper.Map<IEnumerable<UserModel>>(accessModel);
            return response;
        }

        public async Task<UserModel> GetByIdAsync(string id)
        {
            var accessModel = await _userDataAccess.GetByIdAsync(id);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        public async Task<UserModel> UpdateAsync(UpdateUserRequest updateUserRequest)
        {
            var dataAccess = _mapper.Map<UserDataAccessRequest>(updateUserRequest);
            var accessModel = await _userDataAccess.UpdateAsync(dataAccess);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }

        private async Task<bool> CheckUserExistsAsync(RegisterUserRequest createUser)
        {
            var users = await _userDataAccess.GetAll();
            var result = users.Where(x => x.Document == createUser.Document || x.Email == createUser.Email);
            return users.Any();
        }

        public async Task<UserModel> ApproveNewUserAsync(string id)
        {
            var accessModel = await _userDataAccess.ApproveNewUserAsync(id);
            var response = _mapper.Map<UserModel>(accessModel);
            return response;
        }
    }
}
