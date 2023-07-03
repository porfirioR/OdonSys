﻿using Access.Contract.Clients;
using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    public sealed class UserAccess : IUserDataAccess
    {
        private readonly DataContext _context;
        private readonly IUserDataBuilder _userDataBuilder;
        private readonly IClientDataBuilder _clientDataBuilder;

        public UserAccess(DataContext context, IUserDataBuilder userDataBuilder, IClientDataBuilder clientDataBuilder)
        {
            _context = context;
            _userDataBuilder = userDataBuilder;
            _clientDataBuilder = clientDataBuilder;
        }

        public async Task<UserDataAccessModel> ApproveNewUserAsync(string id)
        {
            var entity = await _context.Set<User>()
                            .SingleAsync(x => x.Id == new Guid(id)) ?? throw new KeyNotFoundException($"id {id}");

            entity.Approved = true;
            await _context.SaveChangesAsync();
            return _userDataBuilder.MapUserToUserDataAccessModel(entity);
        }

        public async Task<IEnumerable<string>> SetUserRolesAsync(UserRolesAccessRequest accessRequest)
        {
            var userRoles = await _context.UserRoles
                                .AsNoTracking()
                                .Include(x => x.Role)
                                .AsNoTracking()
                                .Where(x => x.UserId == new Guid(accessRequest.UserId))
                                .ToListAsync();

            var currentRolesOfUser = userRoles.Select(x => x.Role);
            var roleCodes = currentRolesOfUser.Select(x => x.Code);
            var persistRoles = currentRolesOfUser.Where(x => accessRequest.Roles.Contains(x.Code));
            var deleteRoleIds = currentRolesOfUser.Where(x => !accessRequest.Roles.Contains(x.Code)).Select(x => x.Id);


            var newRoleCodes = accessRequest.Roles.Where(x => !roleCodes.Contains(x));
            var allRoles = await _context.Roles
                                .AsNoTracking()
                                .ToListAsync();

            var newRoles = allRoles.Where(x => newRoleCodes.Contains(x.Code));
            var newUserRoles = newRoles.Select(x => new UserRole() { RoleId = x.Id, UserId = new Guid(accessRequest.UserId) });
            _context.ChangeTracker.Clear();

            if (newUserRoles.Any())
            {
                _context.UserRoles.AddRange(newUserRoles);
            }
            if (deleteRoleIds.Any())
            {
                var deleteUserRoles = userRoles.Where(x => deleteRoleIds.Contains(x.RoleId));
                _context.UserRoles.RemoveRange(deleteUserRoles);
            }
            if (newUserRoles.Any() || deleteRoleIds.Any())
            {
                await _context.SaveChangesAsync();
            }
            var rolesIds = newUserRoles.Select(x => x.RoleId).Concat(persistRoles.Select(x => x.Id));
            var userRoleCodes = allRoles.Where(x => rolesIds.Contains(x.Id)).Select(x => x.Code);

            return userRoleCodes;
        }

        public async Task<IEnumerable<DoctorDataAccessModel>> GetAllAsync()
        {
            var response = await _context.Set<User>().ToListAsync();
            var doctorDataAccessModelList = response.Select(_userDataBuilder.MapUserToDoctorDataAccessModel);
            return doctorDataAccessModelList;
        }

        public async Task<IEnumerable<UserDataAccessModel>> GetAllUserAsync()
        {
            var entities = await _context.Set<User>().ToListAsync();
            return entities.Select(_userDataBuilder.MapUserToUserDataAccessModel);
        }

        public async Task<DoctorDataAccessModel> GetByIdAsync(string id)
        {
            var entity = await GetUserByIdAsync(id);
            var doctorDataAccessModelList = _userDataBuilder.MapUserToDoctorDataAccessModel(entity);
            return doctorDataAccessModelList;
        }

        public async Task<UserClientAccessModel> GetUserClientAsync(UserClientAccessRequest accessRequest)
        {
            var entity = await _context.Set<UserClient>()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(accessRequest.UserId) && x.ClientId == new Guid(accessRequest.ClientId));

            return entity is not null ? new UserClientAccessModel(entity.Id, entity.ClientId, entity.UserId) : null;
        }

        public async Task<IEnumerable<UserClientAccessModel>> GetUserClientsByUserIdAsync(string userId)
        {
            var entities = await _context.Set<UserClient>()
                                .Where(x => x.UserId == new Guid(userId))
                                .ToListAsync();

            var response = entities.Select(x => new UserClientAccessModel(x.Id, x.ClientId, x.UserId)).ToList();
            return response;
        }

        public async Task<UserClientAccessModel> CreateUserClientAsync(UserClientAccessRequest accessRequest)
        {
            var entity = _clientDataBuilder.MapUserClientAccessRequestToUserClient(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return new UserClientAccessModel(entity.Id, entity.ClientId, entity.UserId);
        }

        public async Task<DoctorDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess)
        {
            var entity = await GetUserByIdAsync(dataAccess.Id);
            entity = _userDataBuilder.MapUserDataAccessRequestToUser(dataAccess, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var model = _userDataBuilder.MapUserToDoctorDataAccessModel(entity);
            return model;
        }

        private async Task<User> GetUserByIdAsync(string id)
        {
            var entity = await _context.Set<User>()
                            .Include(x => x.UserRoles)
                            .ThenInclude(x => x.Role)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            return entity ?? throw new KeyNotFoundException($"id {id}");
        }
    }
}
