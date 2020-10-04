using DomainServices.Admin.Interfaces;
using OdonSys.Storage.Sql;
using OdonSys.Storage.Sql.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DomainServices.Admin
{
    internal class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.Active = false;
                await _context.AddAsync(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string id)
        {
            var userToDelete = _context.Set<User>().SingleOrDefault(x => x.Id == id);
            userToDelete.Active = false;
            _context.Update(userToDelete);
            _context.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _context.Set<User>().AsQueryable();
        }

        public User GetUserById(string id)
        {
            return _context.Set<User>().SingleOrDefault(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(User userUpdate)
        {
            _context.Update(userUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
