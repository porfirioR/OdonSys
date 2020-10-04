using AutoMapper;
using AutoMapper.QueryableExtensions;
using DomainServices.Admin.Interfaces;
using OdonSys.Api.Main.DTO.Users;
using OdonSys.Api.Main.Flow.Interfaces;
using OdonSys.Middleware.Users;
using OdonSys.Storage.Sql.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdonSys.Api.Main.Flow
{
    public class UserFlow : IUserFlow
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserFlow(IUserService usersServices, IMapper mapper)
        {
            _userService = usersServices;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateUserDTO userDTO)
        {
            var userCommand = _mapper.Map<CreateUserMiddleware>(userDTO);
            var user = _mapper.Map<User>(userDTO);
            return await _userService.CreateAsync(user);
        }

        public ICollection<UpdateUserDTO> GetAll()
        {
            var users = _userService.GetAll();
            var usersCommands = users.ProjectTo<UpdateUserMiddleware>(_mapper.ConfigurationProvider);
            var usersDto = _mapper.Map<ICollection<UpdateUserDTO>>(usersCommands);
            //var usersDto = _mapper.Map<IEnumerable<UpdateUserDTO>>(usersCommands);
            return usersDto;
        }

        public UpdateUserDTO GetById(string id)
        {
            var user = _userService.GetUserById(id);
            var userCommand = _mapper.Map<UpdateUserMiddleware>(user);
            var userDto = _mapper.Map<UpdateUserDTO>(userCommand);
            return userDto;
        }

        public async Task<bool> UpdateAsync(UpdateUserDTO userDTO)
        {
            var userUpdateMiddle = _mapper.Map<UpdateUserMiddleware>(userDTO);
            var userUpdate = _mapper.Map<User>(userUpdateMiddle);
            return await _userService.UpdateAsync(userUpdate);
        }

        public void Delete(string id)
        {
            _userService.Delete(id);
        }

    }
}
