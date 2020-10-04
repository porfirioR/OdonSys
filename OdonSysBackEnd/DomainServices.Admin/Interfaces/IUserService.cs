using OdonSys.Storage.Sql.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DomainServices.Admin.Interfaces
{
    public interface IUserService
    {
        void Delete(string id);
        User GetUserById(string id);
        IQueryable<User> GetAll();
        Task<bool> CreateAsync(User user);
        Task<bool> UpdateAsync(User userUpdate);
    }
}
