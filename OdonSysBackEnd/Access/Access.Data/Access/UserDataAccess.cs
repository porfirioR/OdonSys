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

        public async Task<UserDataAccessModel> DeleteAsync(string id)
        {
            var entity = await GetDoctorByIdAsync(id);
            entity.Active = false;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDataAccessModel>(entity);
        }

        public async Task<IEnumerable<UserDataAccessModel>> GetAll()
        {
            var response = await _context.Set<Doctor>()
                                .ProjectTo<UserDataAccessModel>(_mapper.ConfigurationProvider).ToListAsync();
            return response;
        }

        public async Task<UserDataAccessModel> GetByIdAsync(string id)
        {
            var entity = await GetDoctorByIdAsync(id);
            var response = _mapper.Map<UserDataAccessModel>(entity);
            return response;
        }

        public async Task<UserDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess)
        {
            var entity = GetDoctorByIdAsync(dataAccess.Id);
            entity = _mapper.Map(dataAccess, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var model = _mapper.Map<UserDataAccessModel>(entity);
            return model;
        }

        private async Task<Doctor> GetDoctorByIdAsync(string id)
        {
            var entity = await _context.Set<Doctor>()
                            .Include(x => x.User)
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            return entity ?? throw new KeyNotFoundException($"id {entity.Id}");
        }
    }
}
