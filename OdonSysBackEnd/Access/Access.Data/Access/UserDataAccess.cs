using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Access.Admin.Access
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UserDataAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDataAccessModel> CreateAsync(UserDataAccessRequest dataAccess)
        {
            var entity = _mapper.Map<Doctor>(dataAccess);

            using var hmac = new HMACSHA512();
            var entityUser = new User
            {
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataAccess.Password)),
                PasswordSalt = hmac.Key,
                UserName = @$"{entity.Name[..1].ToUpper()}{entity.LastName}"[..20],
                Approved = false,
            };
            entity.User = entityUser;
            await _context.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                var user = _mapper.Map<UserDataAccessModel>(entity);
                return user;
            }
            throw new Exception("Error al intentar crear usuario.");
        }

        public async Task<UserDataAccessModel> DeleteAsync(string id)
        {
            var model = _context.Set<Doctor>().SingleOrDefault(x => x.Id == new Guid(id));
            if (model is null)
            {
                throw new KeyNotFoundException($"id {id}");
            }
            model.Active = false;
            _context.Update(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDataAccessModel>(model);
        }

        public async Task<IEnumerable<UserDataAccessModel>> GetAll()
        {
            var response = await _context.Set<Doctor>().Where(u => u.Active).ProjectTo<UserDataAccessModel>(_mapper.ConfigurationProvider).ToListAsync();
            return response;
        }

        public async Task<UserDataAccessModel> GetById(string id)
        {
            var entity = await _context.Set<Doctor>().SingleOrDefaultAsync(x => x.Id == new Guid(id));
            var response = _mapper.Map<UserDataAccessModel>(entity);
            return response ?? throw new KeyNotFoundException($"id {id}"); ;
        }

        public async Task<UserDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess)
        {
            var entity = await _context.Set<Doctor>().SingleOrDefaultAsync(x => x.Id == new Guid(dataAccess.Id));
            if (entity is null)
            {
                throw new KeyNotFoundException($"id {entity.Id}");
            }
            entity = _mapper.Map(dataAccess, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var model = _mapper.Map<UserDataAccessModel>(entity);
            return model;
        }
    }
}
