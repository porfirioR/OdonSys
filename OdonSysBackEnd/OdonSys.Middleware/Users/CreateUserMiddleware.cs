using System;

namespace OdonSys.Middleware.Users
{
    public class CreateUserMiddleware
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Active { get; set; }
    }
}
