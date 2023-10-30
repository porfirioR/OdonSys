using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Host.Api.Controllers
{
    public class OdonSysBaseController : ControllerBase
    {
        public string UserId => User.FindFirst(Claims.UserId)?.Value ?? string.Empty;
        public string Username => GetUsername();
        public string UserIdAadB2C => User.FindFirst(Claims.UserIdAadB2C)?.Value ?? string.Empty;

        private string GetUsername()
        {
            var username = User.FindFirst(Claims.UserName)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                username = Helper.GetUsername(User.FindFirst(Claims.NameAadB2C)?.Value, User.FindFirst(Claims.SurnameAadB2C)?.Value);
            }
            return username;
        }
    }
}
