using Access.Contract.Authentication;
using Access.Contract.Users;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Utilities;
using Utilities.Configurations;

namespace Access.Data.Access
{
    internal sealed class AuthenticationAccess : IAuthenticationAccess
    {
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;
        private readonly IUserDataAccessBuilder _userDataBuilder;
        private readonly string _doctorRoleCode;
        private readonly string _adminRoleCode;

        public AuthenticationAccess(IOptions<SystemSettings> systemSettingsOptions, DataContext context, IUserDataAccessBuilder userDataBuilder)
        {
            var systemSettings = systemSettingsOptions.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(systemSettings.TokenKey));
            _context = context;
            _userDataBuilder = userDataBuilder;
            _doctorRoleCode = systemSettings.DoctorRole;
            _adminRoleCode = systemSettings.AdminRole;
        }

        public async Task<AuthenticationAccessModel> LoginAsync(LoginDataAccess loginAccess)
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
            var authAccessModel = new AuthenticationAccessModel(userDataAccessModel, token, expirationDate, JwtBearerDefaults.AuthenticationScheme);
            return authAccessModel;
        }

        public async Task<UserDataAccessModel> RegisterAzureAdB2CUserAsync(UserDataAccessRequest dataAccess)
        {
            var request = _userDataBuilder.MapUserDataAccessRequestToUser(dataAccess);
            var entity = await _context.Users
                .FirstOrDefaultAsync(x => x.Document == request.Document && string.IsNullOrEmpty(x.ExternalUserId));
            if (entity != null)
            {
                entity.ExternalUserId = request.ExternalUserId;
                _context.Users.Update(entity);
            }
            else
            {
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Code == _doctorRoleCode);
                entity = request;
                request.UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        User = entity,
                        Role = role
                    }
                };
                await _context.AddAsync(entity);

            }
            await _context.SaveChangesAsync();
            var userAccessModel = _userDataBuilder.MapUserToUserDataAccessModel(entity);
            var roleCodes = await RoleCodesAsync(entity.Id);
            userAccessModel.Roles = roleCodes;
            return userAccessModel;
        }

        public async Task<AuthenticationAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess)
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

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Code == _doctorRoleCode);
            if (!existUser)
            {
                entity.Approved = true;
                role = await _context.Roles.FirstOrDefaultAsync(x => x.Code == _adminRoleCode) ?? throw new Exception("No existe rol administrativo.");
            }
            if (role is null)
            {
                throw new Exception($"Rol: {CultureInfo.InvariantCulture.TextInfo.ToTitleCase(_doctorRoleCode)} no fue encontrado, favor contacte con soporte.");
            }
            entity.UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    User = entity,
                    Role = role
                }
            };

            await _context.AddAsync(entity);
            if (await _context.SaveChangesAsync() > 0)
            {
                var userAccessModel = _userDataBuilder.MapUserToUserDataAccessModel(entity);
                var roleCodes = await RoleCodesAsync(entity.Id);
                userAccessModel.Roles = roleCodes;
                (string token, DateTime expirationDate) = CreateToken(userAccessModel.UserName, userAccessModel.Id, roleCodes);
                var userResponse = new AuthenticationAccessModel(userAccessModel, token, expirationDate, JwtBearerDefaults.AuthenticationScheme);
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
