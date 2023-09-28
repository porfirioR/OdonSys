using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Utilities;
using Utilities.Configurations;

namespace Host.Api
{
    public partial class Startup
    {
        private const string _policyName = "AzureAd_OR_JwtBearer";
        public void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(_policyName)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RoleClaimType = Claims.UserRoles
                    };
                })
                .AddPolicyScheme(_policyName, _policyName, options => PolicySchemeOptions(options))
                ;

            services
                .AddAuthentication(Constants.AzureAdB2C)
                .AddMicrosoftIdentityWebApi(configuration, AuthenticationSettings.ConfigSection, Constants.AzureAdB2C);

        }
        private void PolicySchemeOptions(PolicySchemeOptions options)
        {
            options.ForwardDefaultSelector = context =>
            {
                var token = string.Empty;
                if (context.Request.Headers.TryGetValue("Authorization", out var value))
                {
                    string authorization = value;
                    if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        token = authorization["Bearer ".Length..].Trim();
                    }
                    else if (authorization.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }
                }

                var jwtHandler = new JwtSecurityTokenHandler();
                if (jwtHandler.CanReadToken(token))
                {
                    var jwtToken = jwtHandler.ReadJwtToken(token);
                    if (jwtToken.Issuer.Equals(MainConfiguration.Authentication.OAuth2.Issuer, StringComparison.OrdinalIgnoreCase))
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }
                }
                return Constants.AzureAdB2C;
            };
        }

    }
}
