using Access.Contract.ClientProcedure;
using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Access.Admin.Access
{
    public class UserAccess : IUserDataAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDataAccessModel> ApproveNewUserAsync(string id)
        {
            var entity = await GetUserByIdAsync(id);
            entity.Approved = true;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDataAccessModel>(entity);
        }

        public async Task<IEnumerable<DoctorDataAccessModel>> GetAllAsync()
        {
            var response = await _context.Set<User>()
                                .ProjectTo<DoctorDataAccessModel>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return response;
        }

        public async Task<IEnumerable<UserDataAccessModel>> GetAllUserAsync()
        {
            var entity = await _context.Set<User>().ToListAsync();
            return _mapper.Map<IEnumerable<UserDataAccessModel>>(entity);
        }

        public async Task<DoctorDataAccessModel> GetByIdAsync(string id)
        {
            var entity = await GetUserByIdAsync(id);
            var response = _mapper.Map<DoctorDataAccessModel>(entity);
            return response;
        }

        public async Task<UserClientAccessModel> GetUserClientAsync(UserClientAccessRequest accessRequest)
        {
            var entity = await _context.Set<UserClient>()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(accessRequest.UserId) && x.ClientId == new Guid(accessRequest.ClientId));

            return entity is null ? null : new UserClientAccessModel(entity.Id, entity.ClientId, entity.UserId);
        }

        public async Task<IEnumerable<UserClientAccessModel>> GetUserClientsByUserIdAsync(string userId)
        {
            var entities = await _context.Set<UserClient>().Where(x => x.UserId == new Guid(userId)).ToListAsync();
            var response = entities.Select(x => new UserClientAccessModel(x.Id, x.ClientId, x.UserId)).ToList();
            return response;
        }

        public async Task<UserClientAccessModel> CreateUserClientAsync(UserClientAccessRequest accessRequest)
        {
            var entity = _mapper.Map<UserClient>(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return new UserClientAccessModel(entity.Id, entity.ClientId, entity.UserId);
        }

        public async Task<DoctorDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess)
        {
            var entity = await GetUserByIdAsync(dataAccess.Id);
            entity = _mapper.Map(dataAccess, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var model = _mapper.Map<DoctorDataAccessModel>(entity);
            return model;
        }

        private async Task<User> GetUserByIdAsync(string id)
        {
            var entity = await _context.Set<User>()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            return entity ?? throw new KeyNotFoundException($"id {id}");
        }
    }
}
