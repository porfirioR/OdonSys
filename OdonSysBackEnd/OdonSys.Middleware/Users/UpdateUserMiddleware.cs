using System;

namespace OdonSys.Middleware.Users
{
    public class UpdateUserMiddleware : CreateUserMiddleware
    {
        public string Id { get; set; }
    }
}
