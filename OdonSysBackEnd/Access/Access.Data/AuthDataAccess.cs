using Access.Contract.Auth;
using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Access.Admin
{
    internal class AuthDataAccess : IAuthDataAccess
    {
        private readonly IMapper _mapper;
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;

        public AuthDataAccess(IMapper mapper, IConfiguration configuration, DataContext context)
        {
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            _context = context;
        }

        public async Task<AuthAccessModel> LoginAsync(LoginDataAccess loginAccess)
        {
            var user = await _context.Users
                            .Include(y => y.Doctor)
                            .FirstOrDefaultAsync(x => x.Doctor.Email == loginAccess.Email);

            if (user is null)
            {
                throw new KeyNotFoundException("Correo y/o contraseña es incorrecta");
            }
            if (!user.Approved)
            {
                throw new UnauthorizedAccessException("Cuenta aún no ha sido aprobada, contacte con el administrador e intente devuelta.");
            }
            if (!user.Doctor.Active)
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
            var token = CreateToken(user.UserName, user.Id.ToString());
            var userAccessModel = _mapper.Map<UserDataAccessModel>(user.Doctor);
            var userResponse = new AuthAccessModel
            {
                Token = token,
                User = userAccessModel
            };
            return userResponse;
        }

        public async Task<AuthAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess)
        {
            var entity = _mapper.Map<Doctor>(dataAccess);
            var userName = @$"{entity.Name[..1].ToUpper()}{entity.LastName}";
            userName = userName.Length > 20 ? userName[..20] : userName;
            using var hmac = new HMACSHA512();
            var entityUser = new User
            {
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataAccess.Password)),
                PasswordSalt = hmac.Key,
                UserName = userName,
                Approved = false,
            };
            entity.User = entityUser;
            await _context.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                var userAccessModel = _mapper.Map<UserDataAccessModel>(entity);
                var token = CreateToken(userAccessModel.UserName, userAccessModel.Id);
                var userResponse = new AuthAccessModel
                {
                    Token = token,
                    User = userAccessModel
                };
                return userResponse;
            }
            throw new Exception("Error al intentar crear usuario.");
        }

        private string CreateToken(string userName, string userId)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, userName),
                new Claim(Claims.UserId, userId)
            };
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
    }
}
