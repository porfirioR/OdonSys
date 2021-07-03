using Resources.Contract.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Resources.Contract.User
{
    public interface IUserManager
    {
        Task<UserModel> Create(CreateUserRequest createUserRequest);
        Task<UserModel> Delete(string id);
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(string id);
        Task<UserModel> Update(UpdateUserRequest updateUserRequest);
        Task<UserModel> Login(LoginRequest login);
    }
}
