﻿using Microsoft.AspNetCore.Authorization;

namespace Host.Api.Models.Auth
{
    public static class AuthorizationExtensions
    {
        public static bool IsAuthenticated(this AuthorizationHandlerContext context)
        {
            if (context.User?.Identity == null)
                return false;

            return context.User.Identity.IsAuthenticated;
        }
    }
}
