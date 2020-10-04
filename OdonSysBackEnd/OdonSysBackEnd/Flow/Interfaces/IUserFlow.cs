using OdonSys.Api.Main.DTO.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdonSys.Api.Main.Flow.Interfaces
{
    public interface IUserFlow
    {
        ICollection<UpdateUserDTO> GetAll();
        Task<bool> UpdateAsync(UpdateUserDTO userDTO);
        Task<bool> CreateAsync(CreateUserDTO userDTO);
        UpdateUserDTO GetById(string id);
        void Delete(string id);
    }
}
