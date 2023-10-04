using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Utilities;
using Utilities.Configurations;
using Utilities.Enums;

namespace Host.Api
{
    public partial class Startup
    {
        private const string _policyName = "AzureAdB2C_OR_JwtBearer";
        public void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("Environment");
            if (string.Equals(environment, OdonSysEnvironment.Test.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                services.AddAuthentication(_policyName)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MainConfiguration.SystemSettings.TokenKey)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            RoleClaimType = Claims.UserRoles
                        };
                    })
                    .AddPolicyScheme(_policyName, _policyName, options => PolicySchemeOptions(options));
            }
            else
            {
                services.AddMicrosoftIdentityWebApiAuthentication(configuration, AzureB2CSettings.ConfigSection)
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddInMemoryTokenCaches();

                services.Configure<JwtBearerOptions>(Constants.AzureAdB2C, options =>
                {
                    options.TokenValidationParameters.RoleClaimType = Claims.UserRoles;
                    options.TokenValidationParameters.ValidAudiences = new[]
                    {
                        MainConfiguration.Authentication.AzureAdB2C.ApiApplicationId
                    };
                });
                services.AddInMemoryTokenCaches();
            }
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
                    if (jwtToken.Issuer.Equals(MainConfiguration.SystemSettings.TokenKey, StringComparison.OrdinalIgnoreCase))
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }
                }
                return Constants.AzureAdB2C;
            };
        }

    }
}
