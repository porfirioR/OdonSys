﻿using Access.Contract.Auth;
using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Utilities;

namespace Access.Data.Access
{
    internal sealed class AuthAccess : IAuthAccess
    {
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;
        private readonly IUserDataAccessBuilder _userDataBuilder;
        private readonly string _doctorRoleCode;
        private readonly string _doctorRoleName;
        private readonly string _adminRoleCode;
        private readonly string _adminRoleName;

        public AuthAccess(IConfiguration configuration, DataContext context, IUserDataAccessBuilder userDataBuilder)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            _context = context;
            _userDataBuilder = userDataBuilder;
            _doctorRoleCode = configuration["Role.code"];
            _doctorRoleName = configuration["Role.name"];
            _adminRoleCode = configuration["RoleAdmin.code"];
            _adminRoleName = configuration["RoleAdmin.name"];
        }

        public async Task<AuthAccessModel> LoginAsync(LoginDataAccess loginAccess)
        {
            var user = await _context.Users
                            .Include(x => x.UserRoles)
                            .ThenInclude(x => x.Role)
                            .FirstOrDefaultAsync(x => x.Email == loginAccess.Email || x.UserName == loginAccess.Email) ??
                            throw new KeyNotFoundException("correo y/o contraseña es incorrecta");

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
            var userId = user.Id;
            var roleCodes = await RoleCodesAsync(userId);
            (string token, DateTime expirationDate) = CreateToken(user.UserName, userId.ToString(), roleCodes);
            var userDataAccessModel = _userDataBuilder.MapUserToUserDataAccessModel(user);
            var authAccessModel = new AuthAccessModel(userDataAccessModel, token, expirationDate, JwtBearerDefaults.AuthenticationScheme);
            return authAccessModel;
        }

        public async Task<AuthAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess)
        {
            var entity = _userDataBuilder.MapUserDataAccessRequestToUser(dataAccess);
            var userName = @$"{entity.Name[..1].ToUpper()}{entity.Surname}";
            userName = userName.Length > 20 ? userName[..20] : userName;
            using var hmac = new HMACSHA512();

            entity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataAccess.Password));
            entity.PasswordSalt = hmac.Key;
            entity.UserName = userName;
            entity.Approved = false;
            entity.IsDoctor = true;
            entity.Active = true;
            var existUser = await _context.Users
                                    .AsNoTracking()
                                    .AnyAsync();

            var doctorRole = await _context.Roles.FirstOrDefaultAsync(x => x.Code == _doctorRoleCode);
            doctorRole ??= new Role()
            {
                Id = Guid.NewGuid(),
                Active = true,
                Code = _doctorRoleCode,
                Name = _doctorRoleName
            };
            if (!existUser)
            {
                entity.Approved = true;
                var adminRole = await _context.Roles.FirstOrDefaultAsync(x => x.Code == _adminRoleCode);
                adminRole ??= new Role()
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    Code = _adminRoleCode,
                    Name = _adminRoleName
                };
                await _context.Roles.AddAsync(adminRole);
                entity.UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        User = entity,
                        RoleId = adminRole.Id
                    }
                };
            }
            else
            {
                await _context.Roles.AddAsync(doctorRole);
                entity.UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        User = entity,
                        RoleId = doctorRole.Id
                    }
                };
            }

            await _context.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                var userAccessModel = _userDataBuilder.MapUserToUserDataAccessModel(entity);
                var roleCodes = await RoleCodesAsync(entity.Id);
                (string token, DateTime expirationDate) = CreateToken(userAccessModel.UserName, userAccessModel.Id, roleCodes);
                userAccessModel.Roles = roleCodes;
                var userResponse = new AuthAccessModel(userAccessModel, token, expirationDate, JwtBearerDefaults.AuthenticationScheme);
                return userResponse;
            }
            throw new Exception("Error al intentar crear usuario.");
        }

        public bool RemoveAllClaims(ClaimsPrincipal claimsPrincipal)
        {
            var identity = claimsPrincipal.Identity as ClaimsIdentity;
            identity.RemoveClaim(claimsPrincipal.FindFirst(Claims.UserId));
            identity.RemoveClaim(claimsPrincipal.FindFirst(Claims.UserName));
            return true;
        }

        private (string token, DateTime expirationDate) CreateToken(string userName, string userId, IEnumerable<string> userRoles)
        {
            var claims = new List<Claim>()
            {
                new Claim(Claims.UserName, userName),
                new Claim(Claims.UserId, userId),
            };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(Claims.UserRoles, userRole));
            }
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var expirationDate = DateTime.UtcNow.AddMinutes(120);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDate,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), expirationDate);
        }

        private async Task<IEnumerable<string>> RoleCodesAsync(Guid userId)
        {
            var codes = await _context.UserRoles
                                .Include(x => x.Role)
                                .Where(x => x.UserId.Equals(userId))
                                .Select(x => x.Role.Code)
                                .ToListAsync();
            return codes;
        }
    }
}
