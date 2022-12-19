using Access.Contract.Auth;
using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Access.Data.Access
{
    internal class AuthAccess : IAuthAccess
    {
        private readonly IMapper _mapper;
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;
        private readonly string _roleCode;

        public AuthAccess(IMapper mapper, IConfiguration configuration, DataContext context)
        {
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            _context = context;
            _roleCode = configuration["Role"];
        }

        public async Task<AuthAccessModel> LoginAsync(LoginDataAccess loginAccess)
        {
            var user = await _context.Doctors
                            .FirstOrDefaultAsync(x => x.Email == loginAccess.Email || x.UserName == loginAccess.Email);

            if (user is null)
            {
                throw new KeyNotFoundException("Correo y/o contraseña es incorrecta");
            }
            if (!user.Approved)
            {
                throw new UnauthorizedAccessException("Cuenta aún no ha sido aprobada, contacte con el administrador e intente devuelta.");
            }
            if (!user.Active)
            {
                throw new UnauthorizedAccessException("Su cuenta fue suspendida, contacte con el administrador del sistema.");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginAccess.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    throw new KeyNotFoundException("Correo o contraseña es incorrecta");
                }
            }
            var roleCodes = await RoleCodesAsync(user.Id);
            var token = CreateToken(user.UserName, user.Id.ToString(), roleCodes);
            var userAccessModel = _mapper.Map<UserDataAccessModel>(user);
            var userResponse = new AuthAccessModel
            {
                Token = token,
                User = userAccessModel,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            };
            return userResponse;
        }

        public async Task<AuthAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess)
        {
            var entity = _mapper.Map<User>(dataAccess);
            var userName = @$"{entity.Name[..1].ToUpper()}{entity.LastName}";
            userName = userName.Length > 20 ? userName[..20] : userName;
            using var hmac = new HMACSHA512();

            entity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataAccess.Password));
            entity.PasswordSalt = hmac.Key;
            entity.UserName = userName;
            entity.Approved = false;
            entity.IsDoctor = true;

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Code == _roleCode);
            entity.DoctorRoles = new List<DoctorRoles>
            {
                new DoctorRoles
                {
                    Doctor = entity,
                    Role = role
                }
            };
            await _context.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                var userAccessModel = _mapper.Map<UserDataAccessModel>(entity);
                var roleCodes = await RoleCodesAsync(entity.Id);
                var token = CreateToken(userAccessModel.UserName, userAccessModel.Id, roleCodes);
                userAccessModel.Roles = roleCodes;
                var userResponse = new AuthAccessModel
                {
                    Token = token,
                    User = userAccessModel
                };
                return userResponse;
            }
            throw new Exception("Error al intentar crear usuario.");
        }

        private string CreateToken(string userName, string userId, IEnumerable<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(Claims.UserName, userName),
                new Claim(Claims.UserId, userId)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(Claims.Roles, role));
            }
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<IEnumerable<string>> RoleCodesAsync(Guid userId)
        {
            var codes = await _context.DoctorRoles
                                .Include(x => x.Role)
                                .Where(x => x.RoleId == userId)
                                .Select(x => x.Role.Code)
                                .ToListAsync();
            return codes;
        }
    }
}
