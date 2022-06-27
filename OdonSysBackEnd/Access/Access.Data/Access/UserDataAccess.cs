using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<UserDataAccessModel> ApproveNewUserAsync(string id)
        {
            var entity = await GetDoctorByIdAsync(id);
            entity.User.Approved = true;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDataAccessModel>(entity);
        }

        public async Task<DoctorDataAccessModel> DeactivateRestoreAsync(string id, bool active)
        {
            var entity = await GetDoctorByIdAsync(id);
            entity.Active = active;
            await _context.SaveChangesAsync();
            return _mapper.Map<DoctorDataAccessModel>(entity);
        }

        public async Task<IEnumerable<DoctorDataAccessModel>> GetAllAsync()
        {
            var response = await _context.Set<Doctor>()
                                .ProjectTo<DoctorDataAccessModel>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return response;
        }

        public async Task<DoctorDataAccessModel> GetByIdAsync(string id)
        {
            var entity = await GetDoctorByIdAsync(id);
            var response = _mapper.Map<DoctorDataAccessModel>(entity);
            return response;
        }

        public async Task<DoctorDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess)
        {
            var entity = await GetDoctorByIdAsync(dataAccess.Id);
            var email = entity.Email;
            entity = _mapper.Map(dataAccess, entity);
            entity.Email = email;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var model = _mapper.Map<DoctorDataAccessModel>(entity);
            return model;
        }

        private async Task<Doctor> GetDoctorByIdAsync(string id)
        {
            var entity = await _context.Set<Doctor>()
                            .Include(x => x.User)
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            return entity ?? throw new KeyNotFoundException($"id {id}");
        }
    }
}
