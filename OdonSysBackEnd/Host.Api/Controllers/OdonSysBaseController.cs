﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Host.Api.Controllers
{
    public class OdonSysBaseController : ControllerBase
    {
        public string UserId => User.FindFirst(Claims.UserId)?.Value ?? string.Empty;
        public string UserName => User.FindFirst(Claims.UserName)?.Value ?? string.Empty;
        public List<string> Roles => User.FindAll(Claims.Roles)?.Select(x => x.Value).ToList() ?? new List<string>();
    }
}
