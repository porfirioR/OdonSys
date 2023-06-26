using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Host.Api.Controllers
{
    public class OdonSysBaseController : ControllerBase
    {
        public string UserId => User.FindFirst(Claims.UserId)?.Value ?? string.Empty;
        public string Username => User.FindFirst(Claims.UserName)?.Value ?? string.Empty;
    }
}
